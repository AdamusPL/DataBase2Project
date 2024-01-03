using Microsoft.Extensions.DependencyInjection;

namespace Jsos3.Shared;

public static class SharedModule
{
    public static void AddSharedModule(this IServiceCollection services)
    {
        services.AddTransient<IDummyInterface, DummyImplementation>();
    }
}
