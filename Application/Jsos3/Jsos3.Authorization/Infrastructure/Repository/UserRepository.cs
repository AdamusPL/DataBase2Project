using Jsos3.Authorization.Models;
using Jsos3.Shared.Auth;
using Jsos3.Shared.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Jsos3.Authorization.ViewModels;
using Newtonsoft.Json;
using System.Data;

namespace Jsos3.Authorization.Infrastructure.Repository
{
    public interface IUserRepository
    {
        public Task<bool> ChangePassword(string login, string hashedPasssword);
        public Task<UserLoginInformation?> GetUserLoginInformation(string login);
        public Task<UserDto?> GetUserModel(int id);
        public Task<UserType> GetUserType(int id);
        public Task<bool> Register(RegisterViewModel model);
    }
    public class UserRepository(IDbConnectionFactory _dbConnectionFactory) : IUserRepository
    {
        public async Task<bool> ChangePassword(string login, string hashedPasssword)
        {
            using var _dbConnection = await _dbConnectionFactory.GetOpenAdministrationConnectionAsync();
            string query = "UPDATE UserLoginInformation SET [Password] = @password WHERE [Login] = @login";
            try
            {
                _dbConnection.QueryFirstOrDefault<UserLoginInformation>(query, new { Password = hashedPasssword, Login = login});
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<UserLoginInformation?> GetUserLoginInformation(string login)
        {
            using var _dbConnection = await _dbConnectionFactory.GetOpenAdministrationConnectionAsync();
            string query = "SELECT * FROM UserLoginInformation WHERE Login = @Login";
            return _dbConnection.QueryFirstOrDefault<UserLoginInformation>(query, new { Login = login });
        }

        public async Task<UserDto?> GetUserModel(int id)
        {
            using var _dbConnection = await _dbConnectionFactory.GetOpenAdministrationConnectionAsync();
            string query = "SELECT * FROM [User] WHERE Id = @Id";
            return _dbConnection.QueryFirstOrDefault<UserDto>(query, new { Id = id});
        }

        public async Task<UserType> GetUserType(int id)
        {
            using var _dbConnection = await _dbConnectionFactory.GetOpenAdministrationConnectionAsync();

            var query = "SELECT * FROM Student WHERE UserId = @UserId";
            var student = _dbConnection.QueryFirstOrDefault(query, new { UserId = id });

            if(student != null)
            {
                return UserType.Student;
            }

            query = "SELECT * FROM Lecturer WHERE UserId = @UserId";
            var lecturer = _dbConnection.QueryFirstOrDefault(query, new { UserId = id });

            if (lecturer != null)
            {
                return UserType.Lecturer;
            }

            return UserType.Unknown;
        }

        public async Task<bool> Register(RegisterViewModel model)
        {
            using var _dbConnection = await _dbConnectionFactory.GetOpenAdministrationConnectionAsync();

            var parameters = new
            {
                model.Login,
                PasswordHash = model.Password,
                model.Name,
                model.Surname,
                FieldsOfStudiesIdsJson = JsonConvert.SerializeObject(model.FieldsOfStudies)
            };

            try
            {
                await _dbConnection.ExecuteAsync("[dbo].[RegisterStudent]", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
