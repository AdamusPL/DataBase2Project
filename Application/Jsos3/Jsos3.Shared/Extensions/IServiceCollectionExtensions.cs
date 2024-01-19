using Microsoft.Extensions.DependencyInjection;

namespace Jsos3.Shared.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddProxed<TInterface, TProxy, TImplementation>(
        this IServiceCollection services,
        Func<IServiceProvider, TImplementation, TProxy> proxyFactory,
        Func<IServiceProvider, TImplementation> implementationFactory,
        ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        where TProxy : TInterface
        where TImplementation : TInterface
        where TInterface : class
    {
        var descriptor = new ServiceDescriptor(
            typeof(TInterface),
            p =>
            proxyFactory(p, implementationFactory(p)),
            serviceLifetime);

        services.Add(descriptor);

        return services;
    }
}
