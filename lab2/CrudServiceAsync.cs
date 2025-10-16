using lab2;
using System.Collections;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Threading;

public class CrudServiceAsync<T> : ICrudServiceAsync<T>
    where T : IEntity
{
    private readonly ConcurrentDictionary<Guid, T> _storage = new();
    private readonly string _filePath;
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private readonly AutoResetEvent _autoReset = new(true);
    private readonly object _lock = new();

    public CrudServiceAsync(string filePath)
    {
        _filePath = filePath;
    }

    public async Task<bool> CreateAsync(T element)
    {
        if (_storage.ContainsKey(element.Id)) return false;
        _storage[element.Id] = element;
        return await Task.FromResult(true);
    }

    public async Task<T> ReadAsync(Guid id)
    {
        _storage.TryGetValue(id, out var item);
        return await Task.FromResult(item);
    }

    public async Task<IEnumerable<T>> ReadAllAsync()
        => await Task.FromResult(_storage.Values);

    public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
    {
        var items = _storage.Values
            .Skip((page - 1) * amount)
            .Take(amount)
            .ToList();
        return await Task.FromResult(items);
    }

    public async Task<bool> UpdateAsync(T element)
    {
        if (!_storage.ContainsKey(element.Id)) return false;
        _storage[element.Id] = element;
        return await Task.FromResult(true);
    }

    public async Task<bool> RemoveAsync(T element)
    {
        return await Task.FromResult(_storage.TryRemove(element.Id, out _));
    }

    public async Task<bool> SaveAsync()
    {
        await _semaphore.WaitAsync();
        try
        {
            _autoReset.WaitOne();
            lock (_lock)
            {
                var json = JsonSerializer.Serialize(_storage.Values,
                    new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            _autoReset.Set();
            return true;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public IEnumerator<T> GetEnumerator() => _storage.Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
