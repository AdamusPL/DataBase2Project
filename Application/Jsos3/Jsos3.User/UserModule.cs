using Jsos3.User.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Jsos3.User;

public static class UserModule
{
    public static void AddUserModule(this IServiceCollection services)
    {
        services.AddScoped<IUserEmailRepository, UserEmailRepository>();
    }
}