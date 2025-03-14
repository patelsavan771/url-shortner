using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace UrlShortner.Helpers
{
    public class DapperHelper
    {
        private readonly string _connectionString;

        public DapperHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        // Execute a query that returns a single result (e.g., SELECT COUNT(*))
        public async Task<T> ExecuteScalarAsync<T>(string query, object parameters = null)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                return await connection.ExecuteScalarAsync<T>(query, parameters);
            }
        }

        // Execute a query that returns multiple results (e.g., SELECT * FROM table)
        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string query, object parameters = null)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<T>(query, parameters);
            }
        }

        // Execute a query (e.g., INSERT, UPDATE, DELETE)
        public async Task<int> ExecuteNonQueryAsync(string query, object parameters = null)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                return await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
