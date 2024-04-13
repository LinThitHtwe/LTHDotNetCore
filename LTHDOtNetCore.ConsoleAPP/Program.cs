
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
sqlConnectionStringBuilder.DataSource = "DESKTOP-IF45PH3\\SQLEXPRESS";
sqlConnectionStringBuilder.InitialCatalog = "DotnetTrainingBatch4";
sqlConnectionStringBuilder.UserID = "sa";
sqlConnectionStringBuilder.Password = "root";

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
    Console.WriteLine("ID : "+row["id"]);
    Console.WriteLine("ID : " + row["title"]);
    Console.WriteLine("ID : " + row["author"]);
    Console.WriteLine("ID : " + row["blogContent"]);
}

Console.ReadKey();