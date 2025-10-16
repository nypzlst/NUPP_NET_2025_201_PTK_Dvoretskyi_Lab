using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    interface ICrudServiceAsync<T>: IEnumerable<T>
    {
        public Task<bool> CreateAsync(T element);
        public Task<bool> RemoveAsync(T element);
        public Task<bool> UpdateAsync(T element);
        public Task<bool> SaveAsync();


        public Task<T> ReadAsync(Guid id);
        public Task<IEnumerable<T>> ReadAllAsync();
        public Task<IEnumerable<T>> ReadAllAsync(int page, int amount);

    }
}
