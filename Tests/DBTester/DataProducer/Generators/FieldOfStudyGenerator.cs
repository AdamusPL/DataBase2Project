using Dapper;
using Models;
using Shared;

namespace DataProducer.Generators
{
    internal class FieldOfStudyGenerator : IEntityGenerator
    {

        public void Generate()
        {
            List<FieldOfStudy> fieldsOfStudies = [];

            string path = Path.Combine(Environment.CurrentDirectory, @"Files\", "fieldsOfStudies.csv");
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    fieldsOfStudies.Add(new FieldOfStudy(values[0], int.Parse(values[1]), values[2]));
                }
            }

            using var connection = DBConnectionProvider.SuperAdminConnection();
            connection.Open();
            foreach (var field in fieldsOfStudies)
            {
                var query = "INSERT INTO FieldOfStudy (Name, Degree, FacultyId) VALUES (@Name, @Degree, @FacultyId)";
                connection.Query(query, field);
                Console.WriteLine("New Field Of Study inserted.");
            }
        }
    }
}
