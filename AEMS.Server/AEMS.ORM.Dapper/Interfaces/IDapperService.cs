using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AEMS.ORM.Dapper
{
    public interface IDapperService
    {
        Task<IEnumerable<T>> SqlQueryAsync<T>(string sql, DynamicParameters parameters) where T : class;

        Task<int> SqlExecuteAsync(string sql, DynamicParameters parameters = null);
    }
}
