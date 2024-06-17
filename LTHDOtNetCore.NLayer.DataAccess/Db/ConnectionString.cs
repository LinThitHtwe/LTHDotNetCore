using Microsoft.Data.SqlClient;

namespace LTHDOtNetCore.NLayer.DataAccess
{
    internal class ConnectionString
    {
        public static SqlConnectionStringBuilder sqlConnectionStringBuilder = new()
        {
            DataSource = "DESKTOP-IF45PH3\\SQLEXPRESS",
            InitialCatalog = "DotnetTrainingBatch4",
            UserID = "sa",
            Password = "root",
            TrustServerCertificate = true,
        };
    }
}
