using Jsos3.Absences.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Jsos3.Absences
{
    public static class AbsenceModule
    {
        public static void AddAbsencesModule(this IServiceCollection services)
        {
            services.AddScoped<IAbsencesOfStudentsRepository, AbsencesOfStudentsRepository>();
            services.AddScoped<IGroupDatesRepository, GroupDatesRepository>();
        }

    }
}
