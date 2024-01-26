using Dapper;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProducer
{
    internal class Cleaner
    {
        public void Clean()
        {
            using var connection = DBConnectionProvider.SuperAdminConnection();
            connection.Open();


            var query = "DELETE FROM Email";
            connection.Query(query);

            query = "DELETE FROM UserLoginInformation";
            connection.Query(query);

            query = "DELETE FROM WorkPhone";
            connection.Query(query);

            query = "DELETE FROM Student_FieldOfStudy";
            connection.Query(query);

            query = "DELETE FROM Student";
            connection.Query(query);

            query = "DELETE FROM Administrator";
            connection.Query(query);

            query = "DELETE FROM FieldOfStudy_Course";
            connection.Query(query);

            query = "DELETE FROM Course";
            connection.Query(query);

            query = "DELETE FROM FieldOfStudy";
            connection.Query(query);

            query = "DELETE FROM Faculty";
            connection.Query(query);

            query = "DELETE FROM Lecturer";
            connection.Query(query);

            query = "DELETE FROM [User]";
            connection.Query(query);

            query = "DELETE FROM [Semester]";
            connection.Query(query);

            Console.WriteLine("Records Cleaned");
        }
    }
}
