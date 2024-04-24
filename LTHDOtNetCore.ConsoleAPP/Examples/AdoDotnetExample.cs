using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection.Metadata;
using LTHDOtNetCore.ConsoleAPP.Connections;
using LTHDOtNetCore.ConsoleAPP.Helper;
using static LTHDOtNetCore.ConsoleAPP.Enums.Enum;

namespace LTHDOtNetCore.ConsoleAPP.Examples
{
    public class AdoDotnetExample
    {

        public void GetAll()
        {
            SqlConnection sqlConnection = new(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            Console.WriteLine("----Connection Open----");

            string query = "select * from blog";
            SqlCommand sqlCommand = new(query, sqlConnection);
            SqlDataAdapter sqlDataAdapter = new(sqlCommand);

            DataTable dataTable = new();
            sqlDataAdapter.Fill(dataTable);

            sqlConnection.Close();
            Console.WriteLine("----Connection Close----");

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine("ID : " + row["id"]);
                Console.WriteLine("ID : " + row["title"]);
                Console.WriteLine("ID : " + row["author"]);
                Console.WriteLine("ID : " + row["blogContent"]);
            }
        }

        public void GetById(int id)
        {
            SqlConnection sqlConnection = new(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            Console.WriteLine("----Connection Open----");

            string query = "select * from blog where id = @id";
            SqlCommand sqlCommand = new(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", id);
            SqlDataAdapter sqlDataAdapter = new(sqlCommand);

            DataTable dataTable = new();
            sqlDataAdapter.Fill(dataTable);

            sqlConnection.Close();
            Console.WriteLine("----Connection Close----");

            if (dataTable.Rows.Count == 0)
            {
                Console.WriteLine("No Data Found");
                return;
            }

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine("ID : " + row["id"]);
                Console.WriteLine("ID : " + row["title"]);
                Console.WriteLine("ID : " + row["author"]);
                Console.WriteLine("ID : " + row["blogContent"]);
            }
        }

        public void Create(string title, string author, string content)
        {
            SqlConnection sqlConnection = new(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            Console.WriteLine("----Connection Open----");

            string query = @"INSERT INTO [dbo].[blog]
                            ([title],[author],[blogContent])
                            VALUES
                            (@title,@author,@content)";


            SqlCommand sqlCommand = new(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@title", title);
            sqlCommand.Parameters.AddWithValue("@author", author);
            sqlCommand.Parameters.AddWithValue("@content", content);
            int result = sqlCommand.ExecuteNonQuery();

            PrintData.PrintMutatedStatus(result,ManipulationMethods.create);   

            sqlConnection.Close();
            Console.WriteLine("----Connection Close----");
        }
        public void Update(int id, string title, string author, string content)
        {
            SqlConnection sqlConnection = new(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            Console.WriteLine("----Connection Open----");

            string query = @"Update [dbo].[blog]
                            SET [title] = @title,
                                [author]=@author,
                                [blogContent]=@content
                            WHERE id = @id";

            SqlCommand sqlCommand = new(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", id);
            sqlCommand.Parameters.AddWithValue("@title", title);
            sqlCommand.Parameters.AddWithValue("@author", author);
            sqlCommand.Parameters.AddWithValue("@content", content);
            int result = sqlCommand.ExecuteNonQuery();

            PrintData.PrintMutatedStatus(result, ManipulationMethods.update);

            sqlConnection.Close();
            Console.WriteLine("----Connection Close----");
        }
        public void Delete(int id)
        {
            SqlConnection sqlConnection = new(ConnectionString.sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            Console.WriteLine("----Connection Open----");

            string query = @"Delete From [dbo].[blog]
                            WHERE id = @id";

            SqlCommand sqlCommand = new(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", id);
            int result = sqlCommand.ExecuteNonQuery();

            PrintData.PrintMutatedStatus(result, ManipulationMethods.delete);

            sqlConnection.Close();
            Console.WriteLine("----Connection Close----");
        }

    }

}
