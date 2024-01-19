using Jsos3.Grades.Builders;
using Jsos3.Grades.Infrastructure.Repository;
using Jsos3.Grades.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Jsos3.Grades;

public static class GradesModule
{
    public static void AddGradesModule(this IServiceCollection services)
    {
        services.AddScoped<IGradeAccepter, GradeAccepter>();
        services.AddScoped<IGradeAdder, GradeAdder>();
        services.AddScoped<IStudentGradeService, StudentGradeService>();
        services.AddScoped<IGradeIndexViewModelBuilder, GradeIndexViewModelBuilder>();
        services.AddScoped<ILecturerGradeIndexViewModelBuilder, LecturerGradeIndexViewModelBiulder>();
        services.AddScoped<ILecturerGroupGradeService, LecturerGroupGradeService>();
        services.AddScoped<IGradeRepository, GradeRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
    }
}