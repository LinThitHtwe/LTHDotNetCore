using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection.Metadata;

namespace LTHDOtNetCore.ConsoleAPP
{
    public class AdoDotnetExample
    {
        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder() { 
        DataSource = "DESKTOP-IF45PH3\\SQLEXPRESS",
        InitialCatalog = "DotnetTrainingBatch4",
        UserID = "sa",
        Password = "root"
        };

        public void GetAll()
        {
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            Console.WriteLine("----Connection Open----");

            string query = "select * from blog";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable();
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
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            Console.WriteLine("----Connection Open----");

            string query = "select * from blog where id = @id";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            sqlConnection.Close();
            Console.WriteLine("----Connection Close----");

            if(dataTable.Rows.Count == 0)
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
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            Console.WriteLine("----Connection Open----");

            string query = @"INSERT INTO [dbo].[blog]
                            ([title],[author],[blogContent])
                            VALUES
                            (@title,@author,@content)";


            SqlCommand sqlCommand = new SqlCommand(query,sqlConnection);
            sqlCommand.Parameters.AddWithValue("@title", title);
            sqlCommand.Parameters.AddWithValue("@author", author);
            sqlCommand.Parameters.AddWithValue("@content", content);
            int result = sqlCommand.ExecuteNonQuery();

            Console.WriteLine(result>0 ? "Successfully Created" : "Create Fail");

            sqlConnection.Close();
            Console.WriteLine("----Connection Close----");
        }
        public void Update(int id, string title, string author, string content)
        {
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            Console.WriteLine("----Connection Open----");

            string query = @"Update [dbo].[blog]
                            SET [title] = @title,
                                [author]=@author,
                                [blogContent]=@content
                            WHERE id = @id";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", id);
            sqlCommand.Parameters.AddWithValue("@title", title);
            sqlCommand.Parameters.AddWithValue("@author", author);
            sqlCommand.Parameters.AddWithValue("@content", content);
            int result = sqlCommand.ExecuteNonQuery();

            Console.WriteLine(result > 0 ? "Successfully Updated" : "Update Fail");


            sqlConnection.Close();
            Console.WriteLine("----Connection Close----");
        }
        public void Delete(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            Console.WriteLine("----Connection Open----");

            string query = @"Delete From [dbo].[blog]
                            WHERE id = @id";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", id);
            int result = sqlCommand.ExecuteNonQuery();

            Console.WriteLine(result > 0 ? "Successfully Deleted" : "Delete Fail");

            sqlConnection.Close();
            Console.WriteLine("----Connection Close----");
        }

    }

}
