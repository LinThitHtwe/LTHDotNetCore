using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LTHDOtNetCore.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;

        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T>(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection sqlConnection = new(_connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new(query, sqlConnection);
            if (parameters is not null && parameters.Length > 0)
            {
                var sqlParamsArray = parameters.Select(parameter => new SqlParameter(parameter.Name, parameter.Value)).ToArray();
                sqlCommand.Parameters.AddRange(sqlParamsArray);
            }
            SqlDataAdapter sqlDataAdapter = new(sqlCommand);
            DataTable dataTable = new();
            sqlDataAdapter.Fill(dataTable);

            sqlConnection.Close();

            string jsonString = JsonConvert.SerializeObject(dataTable);
            List<T> list = JsonConvert.DeserializeObject<List<T>>(jsonString)!;
            return list;
        }

        public T QueryFirstOrDefault<T>(string query,params AdoDotNetParameter[]? parameters)
        {
            SqlConnection sqlConnection = new(_connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new(query, sqlConnection);
            if(parameters is not null && parameters.Length > 0)
            {
                var sqlParamsArray = parameters.Select(parameter => new SqlParameter(parameter.Name, parameter.Value)).ToArray();
                sqlCommand.Parameters.AddRange(sqlParamsArray);
            }
            SqlDataAdapter sqlDataAdapter = new(sqlCommand);
            DataTable dataTable = new();
            sqlDataAdapter.Fill(dataTable);

            sqlConnection.Close();

            string json = JsonConvert.SerializeObject(dataTable);
            List<T> list = JsonConvert.DeserializeObject<List<T>>(json)!;
            return list[0];
        }

        public int Execute(string query,params AdoDotNetParameter[]? parameters)
        {
            SqlConnection sqlConnection = new(_connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new(query, sqlConnection);
            if(parameters is not null && parameters.Length > 0)
            {
                sqlCommand.Parameters.AddRange(parameters.Select(parameter => new SqlParameter(parameter.Name, parameter.Value)).ToArray());
            }
            var result = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return result;
        }
    }

    public class AdoDotNetParameter
    {
        public AdoDotNetParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
