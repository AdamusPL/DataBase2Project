using Dapper;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProducer.Generators
{
    internal class SemesterGenerator : IEntityGenerator
    {
        public void Generate()
        {
            using var connection = DBConnectionProvider.SuperAdminConnection();
            connection.Open();

            for (int i = 2010; i <= 2023; i++)
            {
                string id = $"Zimowy {i}/{i + 1}";
                DateTime beginWinter = new System.DateTime(i, 10, 1, 0, 0, 0, 0);
                DateTime endWinter = new System.DateTime(i + 1, 2, 25, 0, 0, 0, 0);
                var query = "INSERT INTO [Semester] (Id, StartDate, EndDate) VALUES (@id, @beginWinter, @endWinter)";
                connection.Query(query, new { id, beginWinter, endWinter });

                id = $"Letni {i}/{i + 1}";
                DateTime beginSummer = new System.DateTime(i + 1, 3, 1, 0, 0, 0, 0);
                DateTime endSummer = new System.DateTime(i + 1, 7, 15, 0, 0, 0, 0);
                query = "INSERT INTO [Semester] (Id, StartDate, EndDate) VALUES (@id, @beginSummer, @endSummer)";
                connection.Query(query, new { id, beginSummer, endSummer });
            }
        }
    }
}
