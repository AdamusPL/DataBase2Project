using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBUnitTests
{
    public class DBConnectionProvider
    {
        public static IDbConnection StudentConnection()
        {

            return new SqlConnection("Server=172.28.0.1;Database=University-Main;User Id=Student;Password=zaq1@WSX;TrustServerCertificate=True");
        }
        public static IDbConnection AdministrationConnection()
        {
            //get
            //{
                return new SqlConnection("Server=172.28.0.1;Database=University-Main;User Id=Administration;Password=zaq1@WSX;TrustServerCertificate=True");
            //}
        }
        public static IDbConnection LecturerConnection()
        {
            //get
            //{
                return new SqlConnection("Server=172.28.0.1;Database=University-Main;User Id=Lecturer;Password=zaq1@WSX;TrustServerCertificate=True");
            //}
        }

        public static IDbConnection SuperAdminConnection()
        {
            //get
            //{
                return new SqlConnection("Server=172.28.0.1;Database=University-Main;User Id=sa;Password=zaq1@WSX;TrustServerCertificate=True");
            //}
        }
    }
}
