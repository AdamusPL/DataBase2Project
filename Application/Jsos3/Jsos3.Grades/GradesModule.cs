using Jsos3.Grades.Builders;
using Microsoft.Extensions.DependencyInjection;
using Jsos3.Grades.Repository;
using Jsos3.Grades.Services;

namespace Jsos3.Grades;

public static class GradesModule
{
    public static void AddGradesModule(this IServiceCollection services)
    {
        services.AddScoped<IGradeAccepter, GradeAccepter>();
        services.AddScoped<IStudentGradeRepository, StudentGradeRepository>();
        services.AddScoped<IStudentGradeService, StudentGradeService>();
        services.AddScoped<IGradeIndexViewModelBuilder, GradeIndexViewModelBuilder>();
        services.AddScoped<ILecturerGradeIndexViewModelBuilder, LecturerGradeIndexViewModelBiulder>();
        services.AddScoped<ILecturerGradePerository, LecturerGradeRepository>();
        services.AddScoped<ILecturerGroupGradeService, LecturerGroupGradeService>();   
    }
}