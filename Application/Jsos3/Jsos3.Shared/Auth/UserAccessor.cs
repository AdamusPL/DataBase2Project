using Microsoft.AspNetCore.Http;

namespace Jsos3.Shared.Auth
{
    internal class UserAccessor(IHttpContextAccessor _httpContextAccessor) : IUserAccessor
    {
        public int Id
        {
            get
            {
                IEnumerable<System.Security.Claims.Claim> claims = _httpContextAccessor.HttpContext.User.Claims;
                var claim = claims?.FirstOrDefault(x => x.Type == "UserId");

                if (claim == null) return -1;

                return int.Parse(claim.Value);
            }
        }

        public UserType Type
        {
            get
            {
                IEnumerable<System.Security.Claims.Claim> claims = _httpContextAccessor.HttpContext.User.Claims;
                var claim = claims?.FirstOrDefault(x => x.Type == "UserType");

                if (claim == null) throw new Exception("User type doesn't exist even though it should");
                if (!int.TryParse(claim.Value, out var userType)) throw new Exception("User type is stored in the wrong format");

                return (UserType)userType;
            }
        }

        public string NameAndSurname
        {
            get
            {
                IEnumerable<System.Security.Claims.Claim> claims = _httpContextAccessor.HttpContext.User.Claims;
                var claim = claims?.FirstOrDefault(x => x.Type == "Name");

                if (claim == null) throw new Exception("User name doesn't exist even though it should");

                return claim.Value;
            }
        }

        public string Login
        {
            get
            {
                IEnumerable<System.Security.Claims.Claim> claims = _httpContextAccessor.HttpContext.User.Claims;
                var claim = claims?.FirstOrDefault(x => x.Type == "Login");

                if (claim == null) throw new Exception("User login doesn't exist even though it should");

                return claim.Value;
            }
        }

    }
}
