using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Cosmo.Crud.Helper.Interface
{
    public interface ICosmoRepository<T> where T : class
    {
        Task<T> GetByIdAsync(string id, string partitionKey);
        Task< List<T>> GetAllItemsAsync();
        Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate, string partitionKey);
        Task<T> CreateItemAsync(T item, string partitionKey);
        Task<T> UpdateItemAsync(string id, T item, string partitionKey);
        Task DeleteItemAsync(string id, string partitionKey);
    }
}
