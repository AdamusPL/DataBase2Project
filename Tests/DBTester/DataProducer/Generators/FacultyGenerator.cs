using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Dapper;

namespace DataProducer.Generators
{
    internal class FacultyGenerator : IEntityGenerator
    {
        public void Generate()
        {
            List<Faculty> faculties = [];

            string path = Path.Combine(Environment.CurrentDirectory, @"Files\", "faculties.csv");
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    faculties.Add(new Faculty(values[0], values[1]));
                }
            }

            using var connection = DBConnectionProvider.SuperAdminConnection();
            connection.Open();
            foreach (var faculty in faculties)
            {
                var query = "INSERT INTO Faculty (Id, Name) VALUES (@Id, @Name)";
                connection.Query(query, faculty);
                Console.WriteLine("New faculty inserted.");
            }
        }
    }
}
