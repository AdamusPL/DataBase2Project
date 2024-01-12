using Jsos3.Grades.Builders;
using Microsoft.Extensions.DependencyInjection;
using Jsos3.Grades.TrashCan;

namespace Jsos3.Grades;

public static class GradesModule
{
public static void AddGradesModule(this IServiceCollection services)
{
    services.AddScoped<IGradeIndexViewModelBuilder, GradeIndexViewModelBuilder>();


    }
}