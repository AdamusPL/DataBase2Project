using Dapper;
using Shared;
using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DataProducer.Generators
{
    internal class GroupGenerator : IEntityGenerator
    {
        public void Generate()
        {
            List<Group> groups = [];
            string path = Path.Combine(Environment.CurrentDirectory, @"Files\", "groups.csv");
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    groups.Add(new Group(values[0], values[1], TimeOnly.Parse(values[2]), TimeOnly.Parse(values[3]), values[4], int.Parse(values[5])));
                }
            }

            using var connection = DBConnectionProvider.SuperAdminConnection();
            connection.Open();
            foreach (var group in groups)
            {
                var query = "SELECT TOP 1 Id FROM Regularity ORDER BY NEWID(); SELECT CAST(SCOPE_IDENTITY() as int)";
                int RegularityId = connection.Query<int>(query).Single();

                query = "SELECT TOP 1 Id FROM GrupType ORDER BY NEWID(); SELECT CAST(SCOPE_IDENTITY() as int)";
                int TypeId = connection.Query<int>(query).Single();

                query = "SELECT TOP 1 Id FROM Course ORDER BY NEWID(); SELECT CAST(SCOPE_IDENTITY() as int)";
                int CourseId = connection.Query<int>(query).Single();

                query = "SELECT TOP 1 Id FROM Semester ORDER BY NEWID(); SELECT CAST(SCOPE_IDENTITY() as int)";
                int SemesterId = connection.Query<int>(query).Single();

                query = "INSERT INTO Group (Id, DayOfTheWeek, StartTime, EndTime, Classroom, Capacity, RegularityId, TypeId, CourseId, SemesterId) " +
                    "VALUES (@Id, @DayOfTheWeek, @StartTime, @EndTime, @Classroom, @Capacity, @RegularityId, @TypeId, @CourseId, @SemesterId)";
                connection.Query(query, new { group.Id, group.DayOfWeek, group.StartTime, group.EndTime, group.Classroom, group.Capacity, RegularityId, TypeId, CourseId, SemesterId });

                Console.WriteLine("New student inserted.");
            }
        }
    }
}
