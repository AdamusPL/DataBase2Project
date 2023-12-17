using Microsoft.Data.SqlClient;
using System.Data;

namespace Shared
{
    public class DBConnectionProvider
    {
        private static IDbConnection GetConnection(string connectionString) =>
            new SqlConnection(connectionString);

        public static IDbConnection StudentConnection() =>
            GetConnection("Server=172.28.0.1;Database=University-Main;User Id=Student;Password=zaq1@WSX;TrustServerCertificate=True");

        public static IDbConnection AdministrationConnection() =>
            GetConnection("Server=172.28.0.1;Database=University-Main;User Id=Administration;Password=zaq1@WSX;TrustServerCertificate=True");
        public static IDbConnection LecturerConnection() =>
            GetConnection("Server=172.28.0.1;Database=University-Main;User Id=Lecturer;Password=zaq1@WSX;TrustServerCertificate=True");

        public static IDbConnection SuperAdminConnection() =>
            GetConnection("Server=172.28.0.1;Database=University-Main;User Id=sa;Password=zaq1@WSX;TrustServerCertificate=True");
    }
}
