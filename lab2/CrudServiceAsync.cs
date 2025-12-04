using lab2;
using System.Collections;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Threading;

public class CrudServiceAsync<T> : ICrudServiceAsync<T>
    where T : class
{
    private IRepository<T> _repository;

    public CrudServiceAsync(IRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task<bool> CreateAsync(T element)
    {
        await _repository.AddAsync(element);
        return true;
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<T>> ReadAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<T?> ReadAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);  
    }

    public async Task<bool> RemoveAsync(T element)
    {
        await _repository.Delete(element);
        return true;
    }

    public async Task<bool> UpdateAsync(T element)
    {
        await _repository.Update(element);
        return true;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
