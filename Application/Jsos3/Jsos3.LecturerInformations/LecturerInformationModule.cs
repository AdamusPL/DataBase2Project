using Jsos3.LecturerInformations.Infrastructure.Repository;
using Jsos3.LecturerInformations.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Jsos3.LecturerInformations;

public static class LecturerInformationsModule
{
    public static void AddLecturerInformationsModule(this IServiceCollection services)
    {
        services.AddScoped<ILecturersDataRepository, LecturersDataRepository>();
        services.AddScoped<ILecturerService, LecturerService>();
    }
}
