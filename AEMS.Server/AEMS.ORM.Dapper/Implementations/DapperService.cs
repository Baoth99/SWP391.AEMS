using AEMS.Utilities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AEMS.ORM.Dapper
{
    public class DapperService : DapperBaseService, IDapperService
    {
        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="DapperService"/> class.
        /// </summary>
        public DapperService() : base()
        {
        }

        #endregion

        #region SqlExecuteAsync

        /// <summary>
        /// SQLs the execute asynchronous.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<int> SqlExecuteAsync(string sql, DynamicParameters parameters = null)
        {
            using (var sqlConnection = new SqlConnection(SqlConnectionString))
            {
                sqlConnection.Open();
                try
                {
                    if (parameters == null)
                    {
                        return await sqlConnection.ExecuteAsync(sql, commandTimeout: AppSettingValues.CommandTimeout);
                    }
                    return await sqlConnection.ExecuteAsync(sql, parameters, commandTimeout: AppSettingValues.CommandTimeout);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

        #endregion

        #region SqlQueryAsync

        /// <summary>
        /// SQLs the query asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<IEnumerable<T>> SqlQueryAsync<T>(string sql, DynamicParameters parameters) where T : class
        {
            string connStr = SqlConnectionString;

            if (AppSettingValues.ReadScaleOut)
            {
                connStr = SqlConnectionString + ";ApplicationIntent=ReadOnly";
            }

            using (var sqlConnection = new SqlConnection(connStr))
            {
                sqlConnection.Open();
                try
                {
                    if (parameters == null)
                    {
                        return await sqlConnection.QueryAsync<T>(sql, commandTimeout: AppSettingValues.CommandTimeout);
                    }
                    return await sqlConnection.QueryAsync<T>(sql, parameters, commandTimeout: AppSettingValues.CommandTimeout);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

        #endregion
    }
}
