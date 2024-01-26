using Dapper;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProducer.Generators
{
    internal class AdministratorGenerator : IEntityGenerator
    {
        public void Generate()
        {
            List<UserExtended> _users = [];
            string path = Path.Combine(Environment.CurrentDirectory, @"Files\", "administrators.csv");
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    _users.Add(new UserExtended(values[0], values[1], values[2], values[3], values[4], values[5]));
                }
            }

            using var connection = DBConnectionProvider.SuperAdminConnection();
            connection.Open();
            foreach (var user in _users)
            {
                var query = "INSERT INTO [User] (Name,Surname) VALUES (@name,@surname); SELECT CAST(SCOPE_IDENTITY() as int)";
                int userId = connection.Query<int>(query, new { user.Name, user.Surname }).Single();

                query = "INSERT INTO Email (UserId, Email) VALUES (@userId,@email)";
                connection.Query(query, new { user.Email, userId });

                query = "INSERT INTO UserLoginInformation (UserId, Login, Password) VALUES (@userId, @Login, @Password)";
                connection.Query(query, new { userId, user.Login, user.Password });

                query = "INSERT INTO WorkPhone (UserId, Phone) VALUES (@userId,@phone)";
                connection.Query(query, new { user.Phone, userId });

                query = "INSERT INTO Administrator (USerId) VALUES (@userId)";
                connection.Query(query, new { userId });

            }
            Console.WriteLine($"{_users.Count} Administrators inserted.");
        }
    }
}
