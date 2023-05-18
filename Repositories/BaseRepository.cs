using System.Data;
using System.Data.SqlClient;
using Dapper;
using static Dapper.SqlMapper;

namespace TesteAPI.Repositories
{
    public class BaseRepository : IDisposable
    {
        protected readonly string connectionString;
        public BaseRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        ~BaseRepository()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        protected string GuidValue { get { return Guid.NewGuid().ToString(); } }

        protected T QueryFirstOrDefault<T>(string sql, object parameters = null, int commandTimeOut = 0)
        {
            using (var connection = ConnectionFactory())
            {
                var q = connection.QueryFirstOrDefault<T>(sql, parameters, commandTimeout: commandTimeOut);

                connection.Close();

                return q;
            }
        }

        protected async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters = null, int commandTimeOut = 0)
        {
            using (var connection = ConnectionFactory())
            {
                var q = await connection.QueryFirstOrDefaultAsync<T>(sql, parameters, commandTimeout: commandTimeOut);

                await connection.CloseAsync();

                return q;
            }
        }

        protected T QuerySingleOrDefault<T>(string sql, object parameters = null, int commandTimeOut = 0)
        {
            using (var connection = ConnectionFactory())
            {
                var q = connection.QuerySingleOrDefault<T>(sql, parameters, commandTimeout: commandTimeOut);

                connection.Close();

                return q;
            }
        }

        protected async Task<T> QuerySingleOrDefaultAsync<T>(string sql, object parameters = null, int commandTimeOut = 0)
        {
            using (var connection = ConnectionFactory())
            {
                var q = await connection.QuerySingleOrDefaultAsync<T>(sql, parameters, commandTimeout: commandTimeOut);

                await connection.CloseAsync();

                return q;
            }
        }

        protected IEnumerable<T> Query<T>(string sql, object parameters = null, int commandTimeOut = 0)
        {
            using (var connection = ConnectionFactory())
            {
                var q = connection.Query<T>(sql, parameters, commandTimeout: commandTimeOut);

                connection.Close();

                return q;
            }
        }

        protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null, int commandTimeOut = 0)
        {
            using (var connection = ConnectionFactory())
            {
                var q = await connection.QueryAsync<T>(sql, parameters, commandTimeout: commandTimeOut);

                await connection.CloseAsync();

                return q;
            }
        }

        protected GridReader QueryMultiple(string sql, object parameters = null, int commandTimeOut = 0)
        {
            var connection = ConnectionFactory();

            var q = connection.QueryMultiple(sql, parameters, commandTimeout: commandTimeOut);

            //connection.Close();

            return q;
        }

        protected async Task<GridReader> QueryMultipleAsync(string sql, object parameters = null, int commandTimeOut = 0)
        {
            var connection = ConnectionFactory();

            var q = await connection.QueryMultipleAsync(sql, parameters, commandTimeout: commandTimeOut);

            //await connection.CloseAsync();

            return q;
        }

        protected int Execute(string sql, object parameters = null, int commandTimeOut = 0)
        {
            using (var connection = ConnectionFactory())
            {
                var q = connection.Execute(sql, parameters, commandTimeout: commandTimeOut);

                connection.Close();

                return q;
            }
        }

        protected async Task<int> ExecuteAsync(string sql, object parameters = null, int commandTimeOut = 0)
        {
            using (var connection = ConnectionFactory())
            {
                var q = await connection.ExecuteAsync(sql, parameters, commandTimeout: commandTimeOut);

                await connection.CloseAsync();

                return q;
            }
        }

        protected T ExecuteScalar<T>(string sql, object parameters = null, int commandTimeOut = 0)
        {
            using (var connection = ConnectionFactory())
            {
                var q = connection.ExecuteScalar<T>(sql, parameters, commandTimeout: commandTimeOut);

                connection.Close();

                return q;
            }
        }

        protected async Task<T> ExecuteScalarAsync<T>(string sql, object parameters = null, int commandTimeOut = 0)
        {
            using (var connection = ConnectionFactory())
            {
                var q = await connection.ExecuteScalarAsync<T>(sql, parameters, commandTimeout: commandTimeOut);

                await connection.CloseAsync();

                return q;
            }
        }

        protected async Task<T> ExecuteScalarTransactionAsync<T>(IDbTransaction transaction, string sql, object parameters = null, int commandTimeOut = 0)
        {
            using (var connection = ConnectionFactory())
            {
                var q = await connection.ExecuteScalarAsync<T>(sql, parameters, commandTimeout: commandTimeOut, transaction: transaction);

                await connection.CloseAsync();

                return q;
            }
        }

        public SqlConnection ConnectionFactory()
        {
            var conn = new SqlConnection(connectionString);

            return conn;
        }
    }
}
