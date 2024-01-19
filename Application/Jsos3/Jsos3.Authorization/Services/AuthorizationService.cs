using Jsos3.Authorization.Infrastructure.Repository;
using Jsos3.Authorization.Models;
using Jsos3.Authorization.ViewModels;
using Jsos3.Shared.Auth;
using Jsos3.Shared.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Jsos3.Authorization.Services
{
    public interface IAuthorizationService
    {
        public Task<User?> AuthenticateUser(string login, string password);

        public string GenerateJSONWebToken(User user);
        public bool UserExists(string login);
        public Task<bool> Register(RegisterViewModel model);
        public Task<bool> ChangePassword(string login, string oldPassword, string newPassword);
    }
    internal class AuthorizationService(SecurityTokenHandler _jwtSecurityTokenHandler, IUserRepository _userRepository, IConfiguration _configuration, PasswordHasher<User> _passwordHasher) : IAuthorizationService
    {

        public bool UserExists(string login)
        {
            return _userRepository.GetUserLoginInformation(login) != null;
        }

        public async Task<User?> AuthenticateUser(string login, string password)
        {
            var user = await _userRepository.GetUserLoginInformation(login);
            if (user == null) return null;

            var hashUser = new User
            {
                Name = user.Login
            };

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(hashUser, user.Password, password);

            if (passwordVerificationResult != PasswordVerificationResult.Success) return null;

            var userInfo = await _userRepository.GetUserModel(user.UserId);
            var usertype = await _userRepository.GetUserType(user.UserId);

            if (userInfo == null) return null;

            return new User
            {
                Id = userInfo.Id,
                Name = userInfo.Name,
                Surname = userInfo.Surname,
                Type = usertype,
                Login = user.Login
            };
        }

        public string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("UserType", ((int)user.Type).ToString()),
                new Claim(ClaimTypes.Role, user.Type.ToString()),
                new Claim("Name", $"{user.Name} {user.Surname}"),
                new Claim("Login", user.Login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return _jwtSecurityTokenHandler.WriteToken(token);
        }

        public async Task<bool> Register(RegisterViewModel model)
        {
            model.Password = _passwordHasher.HashPassword(new User() { Name = model.Login }, model.Password);
            return await _userRepository.Register(model);
        }

        public async Task<bool> ChangePassword(string login, string oldPassword, string newPassword)
        {
            var hashedPasssword = _passwordHasher.HashPassword(new User() { Name = login }, newPassword);

            return await _userRepository.ChangePassword(login, hashedPasssword);
        }
    }
}
