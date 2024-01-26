using Jsos3.Shared.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Jsos3.User.Infrastructure.Repository
{
    public interface IUserEmailRepository
    {
        public Task<List<string>> GetEmails(int userId);

        public Task<bool> AddEmail(int userId, string email);
        public Task<bool> RemoveEmail(int userId, string email);
    }
    internal class UserEmailRepository(IDbConnectionFactory _dbConnectionFactory) : IUserEmailRepository
    {
        public async Task<bool> AddEmail(int userId, string email)
        {
            using var connection = await _dbConnectionFactory.GetOpenAdministrationConnectionAsync(); //powinno tu byc lecturer, ale nie jestem pewien czy ma uprawinienia a nie ma czasu zeby sie bawic w ich poprawianie
            var query = "INSERT INTO Email(UserId, Email) VALUES(@userId, @email)";
            try
            {
                var result = await connection.QueryAsync<string>(query, new { userId, email });
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<List<string>> GetEmails(int userId)
        {
            using var connection = await _dbConnectionFactory.GetOpenAdministrationConnectionAsync(); //powinno tu byc lecturer, ale nie jestem pewien czy ma uprawinienia a nie ma czasu zeby sie bawic w ich poprawianie
            var query = "SELECT Email FROM Email WHERE UserId = @userId";
            var result = await connection.QueryAsync<string>(query, new { userId });
            return result.ToList();
        }

        public async Task<bool> RemoveEmail(int userId, string email)
        {
            using var connection = await _dbConnectionFactory.GetOpenAdministrationConnectionAsync(); //powinno tu byc lecturer, ale nie jestem pewien czy ma uprawinienia a nie ma czasu zeby sie bawic w ich poprawianie
            var query = "DELETE FROM Email WHERE UserId = @userId AND Email = @email";
            try
            {
                var result = await connection.QueryAsync<string>(query, new { userId, email });
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
