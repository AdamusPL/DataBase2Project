using Jsos3.Groups.Helpers;
using Jsos3.Groups.Infrastructure.Repository;
using Jsos3.Groups.Services;
using Jsos3.Groups.ViewModels.Builders;
using Jsos3.Shared.Db;
using Jsos3.Shared.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Jsos3.Groups;

public static class GroupsModule
{
    public static void AddGroupsModule(this IServiceCollection services)
    {
        services.AddProxed<IStudentGroupRepository, CachedStudentGroupRepository, StudentGroupRepository>(
            (p, impl) => new(impl, p.GetRequiredService<IMemoryCache>()),
            p => new(p.GetRequiredService<IDbConnectionFactory>()));

        services.AddProxed<ISemesterRepository, CachedSemesterRepository, SemesterRepository>(
            (p, impl) => new(impl, p.GetRequiredService<IMemoryCache>()),
            p => new(p.GetRequiredService<IDbConnectionFactory>()));

        services.AddTransient<IViewHelper, ViewHelper>();
        services.AddScoped<IGradeRepository, GradeRepository>();
        services.AddScoped<ISemesterSummaryViewModelBuilder, SemesterSummaryViewModelBuilder>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ISemesterService, SemesterService>();
        services.AddScoped<IStudentGroupDtoMapper, StudentGroupDtoMapper>();
        services.AddScoped<IStudentIndexViewModelBuilder, StudentIndexViewModelBuilder>();
    }
}
