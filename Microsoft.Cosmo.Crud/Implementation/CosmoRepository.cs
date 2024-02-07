using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Cosmo.Crud.Helper.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Cosmo.Crud.Helper.Implementation
{
    public class CosmoRepository<T> : ICosmoRepository<T> where T : class
    {
        public readonly Container _container;
        public CosmoRepository(Container container)
        {
            _container = container;
        }
        public async Task<T> GetByIdAsync(string id, string partitionKey)
        {
            try
            {
                var response = await _container.ReadItemAsync<T>(id, new PartitionKey(partitionKey));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate, string partitionKeyValue)
        {
            try
            {

            var queryOptions = new QueryRequestOptions
            {
                PartitionKey = new PartitionKey(partitionKeyValue)
            };

            var queryable = _container.GetItemLinqQueryable<T>(allowSynchronousQueryExecution: false, requestOptions: queryOptions);
            var iterator = queryable.Where(predicate).ToFeedIterator();

            var results = new List<T>();
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                results.AddRange(response.Resource);
            }

            return results;
            }
            catch
            {
                throw;
            }
        }
        public  async Task<List<T>> GetAllItemsAsync()
        {
            try
            {

            var query = new QueryDefinition($"SELECT * FROM c");
       

            var resultSetIterator = _container.GetItemQueryIterator<T>(query);

            var results = new List<T>();

            while (resultSetIterator.HasMoreResults)
            {
                var response = await resultSetIterator.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
            }
            catch
            {
                throw;
            }
        }
        public async Task<T> CreateItemAsync(T item, string partitionKey)
        {
            try
            {

            var response = await _container.CreateItemAsync(item);
            return response.Resource;
            }
            catch
            {
                throw;
            }
        }
        public async Task<T> UpdateItemAsync(string id, T item, string partitionKey)
        {
            try
            {

            var response = await _container.ReplaceItemAsync(item, id, new PartitionKey(partitionKey));
            return response.Resource;
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteItemAsync(string id, string partitionKey)
        {
            try
            {

            await _container.DeleteItemAsync<T>(id, new PartitionKey(partitionKey));
            }
            catch
            {
                throw;
            }
        }
    }
}
