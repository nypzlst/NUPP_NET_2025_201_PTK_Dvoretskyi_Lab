using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public interface ICrudServiceAsync<T>: IEnumerable<T>
    {
        Task<bool> CreateAsync(T element);
        Task<T?> ReadAsync(Guid id);
        Task<IEnumerable<T>> ReadAllAsync();
        Task<bool> UpdateAsync(T element);
        Task<bool> RemoveAsync(T element);

    }
}
