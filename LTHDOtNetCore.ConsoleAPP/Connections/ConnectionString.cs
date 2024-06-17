using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTHDOtNetCore.ConsoleAPP.Connections
{
    public class ConnectionString
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
