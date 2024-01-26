using Dapper;
using Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProducer.Generators
{
    internal class CourseGenerator : IEntityGenerator
    {
        public void Generate()
        {
            List<Course> courses = [];
            string path = Path.Combine(Environment.CurrentDirectory, @"Files\", "courses.csv");
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    courses.Add(new Course(values[0], int.Parse(values[1])));
                }
            }

            using var connection = DBConnectionProvider.SuperAdminConnection();
            connection.Open();
            foreach (var course in courses)
            {
                var query = "SELECT TOP 1 Id FROM Lecturer ORDER BY NEWID(); SELECT CAST(SCOPE_IDENTITY() as int)";
                int lecturerId = connection.Query<int>(query).Single();

                query = "SELECT TOP 1 Id FROM FieldOfStudy ORDER BY NEWID(); SELECT CAST(SCOPE_IDENTITY() as int)";
                int fieldOfStudyId = connection.Query<int>(query).Single();

                query = "INSERT INTO Course (Name, ECTS, LecturerId) VALUES (@Name, @ECTS, @lecturerId); SELECT CAST(SCOPE_IDENTITY() as int)";
                int courseId = connection.Query<int>(query, new { course.Name, course.ECTS, lecturerId }).Single();

                query = "INSERT INTO FieldOfStudy_Course (FieldOfStudyId, CourseId) VALUES (@fieldOfStudyId,@courseId)";
                connection.Query(query, new { fieldOfStudyId, courseId });

            }
            Console.WriteLine($"{courses.Count} Courses inserted.");
        }
    }
}
