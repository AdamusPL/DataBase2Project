using Dapper;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProducer.Generators
{
    internal class GradeGenerator : IEntityGenerator
    {
        public void Generate()
        {
            using var connection = DBConnectionProvider.SuperAdminConnection();
            connection.Open();

            var random = new Random();
            var list = new List<int>
            {
            2,3,4,5
            };

            for (int i = 0; i < 50; i++)
            {
                var number = random.Next(4);
                bool randomBooleanAccepted = random.Next(2) == 0;
                bool randomBooleanIsFinal = random.Next(2) == 0;

                var query = "SELECT Student_Group.Id FROM Student_Group";

                var studentIds = connection.Query<int>(query).ToList();

                var numberS = random.Next(studentIds.Count);
                var randomId = studentIds[numberS];
                var grade = list[number];

                query = "INSERT INTO [Grade] (Grade, Accepted, isFinal, StudentInGroupId) VALUES (@grade, @randomBooleanAccepted, @randomBooleanIsFinal, @randomId)";
                connection.Execute(query, new { grade, randomBooleanAccepted, randomBooleanIsFinal, randomId });
            }
        }
    }
}
