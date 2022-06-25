using AEMS.Utilities;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEMS.MSAzureService
{
    public class CosmosDBService : ICosmosDBService
    {
        private readonly CosmosClient _cosmosClient;

        private Container _container;

        public CosmosDBService()
        {
            _cosmosClient = new CosmosClient(AppSettingValues.CosmosDbConnectionString);
        }

        public void SetCosmosDbInfo(string database, string container)
        {
            _container = _cosmosClient.GetContainer(database, container);
        }

        public async Task AddAsync<T>(T model) where T : BaseCosmosDbModel
        {
            await _container.CreateItemAsync(model, new PartitionKey(model.id));
        }

        public async Task DeleteAsync<T>(string id) where T : BaseCosmosDbModel
        {
            await _container.DeleteItemAsync<T>(id, new PartitionKey(id));
        }

        public async Task UpdateAsync<T>(T model) where T : BaseCosmosDbModel
        {
            await _container.UpsertItemAsync<T>(model, new PartitionKey(model.id));
        }

        public async Task<T> GetAsync<T>(string id) where T : BaseCosmosDbModel
        {
            try
            {
                var response = await _container.ReadItemAsync<T>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException)
            {
                return default;
            }
        }

        public async Task<IEnumerable<T>> GetManyAsync<T>(string queryString) where T : BaseCosmosDbModel
        {
            var query = _container.GetItemQueryIterator<T>(new QueryDefinition(queryString));

            var results = new List<T>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
    }
}
