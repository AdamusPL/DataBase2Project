using Jsos3.Absences.Infrastructure.Repository;
using Jsos3.Absences.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Jsos3.Absences;

public static class AbsenceModule
{
    public static void AddAbsencesModule(this IServiceCollection services)
    {
        services.AddScoped<IAbsencesOfStudentsRepository, AbsencesOfStudentsRepository>();
        services.AddScoped<IGroupDatesRepository, GroupDatesRepository>();
        services.AddScoped<IStudentsInGroupRepository, StudentsInGroupRepository>();
        services.AddScoped<IGroupService, GroupService>();
        services.AddScoped<IGroupOccurrencesCalculator, GroupOccurrencesCalculator>();
    }
}
