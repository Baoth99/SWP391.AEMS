using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEMS.MSAzureService
{
    public interface ICosmosDBService
    {
        void SetCosmosDbInfo(string database, string container);

        Task AddAsync<T>(T model) where T : BaseCosmosDbModel;

        Task DeleteAsync<T>(string id) where T : BaseCosmosDbModel;

        Task UpdateAsync<T>(T model) where T : BaseCosmosDbModel;

        Task<T> GetAsync<T>(string id) where T : BaseCosmosDbModel;

        Task<IEnumerable<T>> GetManyAsync<T>(string queryString) where T : BaseCosmosDbModel;

        IEnumerable<T> GetMany<T>(Func<T, bool> fun, bool isSynchronousQuery = true) where T : BaseCosmosDbModel;

        IEnumerable<T> GetAll<T>(bool isSynchronousQuery = true) where T : BaseCosmosDbModel;

    }
}
