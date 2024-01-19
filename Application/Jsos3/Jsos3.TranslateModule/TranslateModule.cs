using Jsos3.TranslateModule.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Jsos3.TranslateModule;

public static class TranslateModule
{
    public static void AddTranslateModule(this IServiceCollection services)
    {
        services.AddScoped<ITranslationService, PolishTranslationService>();
    }
}