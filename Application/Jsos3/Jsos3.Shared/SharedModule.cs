using Jsos3.Shared.Auth;
using Jsos3.Shared.Db;
using Microsoft.Extensions.DependencyInjection;

namespace Jsos3.Shared;

public static class SharedModule
{

    public static void AddSharedModule(this IServiceCollection services)
    {
        services.AddTransient<IDbConnectionFactory, SqlConnectionFactory>();
        services.AddScoped<IUserAccessor, DummyUserAccessor>();
        services.AddTransient(_ => TimeProvider.System);
    }
}
