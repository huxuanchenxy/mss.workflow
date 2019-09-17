using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using Slickflow.Data;
using System.Data.SqlClient;

namespace Slickflow.Module.Resource.Data
{
    public abstract class BaseRepo
    {
        private readonly string _ConnectionString;

        protected BaseRepo()
        {
            _ConnectionString = ConnectionString.Value;
        }

        protected async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData)
        {
            try
            {
                using (var connection = new MySqlConnection(_ConnectionString))
                {
                    await connection.OpenAsync();
                    return await getData(connection);
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch (MySqlException ex)
            {

                throw new Exception(ex.ToString());
            }
        }

        protected T WithConnection<T>(Func<IDbConnection, T> getData)
        {
            try
            {
                if (ConnectionString.DbType == DatabaseTypeEnum.MYSQL.ToString())
                {
                    using (var connection = new MySqlConnection(_ConnectionString))
                    {
                        connection.OpenAsync();
                        return getData(connection);
                    }
                }
                else if (ConnectionString.DbType == DatabaseTypeEnum.SQLSERVER.ToString())
                {
                    using (var connection = new SqlConnection(_ConnectionString))
                    {
                        connection.OpenAsync();
                        return getData(connection);
                    }
                }
                else
                {
                    using (var connection = new MySqlConnection(_ConnectionString))
                    {
                        connection.OpenAsync();
                        return getData(connection);
                    }
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch (MySqlException ex)
            {

                throw new Exception(ex.ToString());
            }
        }
    }
}
