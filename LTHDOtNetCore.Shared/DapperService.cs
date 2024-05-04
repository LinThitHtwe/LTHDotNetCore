using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace LTHDOtNetCore.Shared
{
    public class DapperService
    {
        private readonly string _connectionString;

        public DapperService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T>(string query,object? param = null)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            var list = dbConnection.Query<T>(query, param).ToList();
            return list;
        }

        public T? QueryFirstOrDefault<T>(string query,object? param = null)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            var item = dbConnection.Query<T>(query, param).FirstOrDefault();
            return item;
        }

        public int Execute(string query, object? param = null)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            var result = dbConnection.Execute(query, param);
            return result;
        }
    }
}
