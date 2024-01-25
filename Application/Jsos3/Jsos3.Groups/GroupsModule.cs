using Jsos3.Groups.Helpers;
using Jsos3.Groups.Infrastructure.Repository;
using Jsos3.Groups.Logic;
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
        services.AddProxed<IGroupRepository, CachedGroupRepository, GroupRepository>(
            (p, impl) => new(impl, p.GetRequiredService<IMemoryCache>()),
            p => new(p.GetRequiredService<IDbConnectionFactory>()));

        services.AddProxed<ISemesterRepository, CachedSemesterRepository, SemesterRepository>(
            (p, impl) => new(impl, p.GetRequiredService<IMemoryCache>()),
            p => new(p.GetRequiredService<IDbConnectionFactory>()));

        services.AddTransient<IViewHelper, ViewHelper>();
        services.AddScoped<IGradeRepository, GradeRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ISemesterSummaryViewModelBuilder, SemesterSummaryViewModelBuilder>();
        services.AddScoped<IGroupFilter, GroupFilter>();
        services.AddScoped<IGroupService, GroupService>();
        services.AddScoped<ISemesterService, SemesterService>();
        services.AddScoped<ILecturerIndexViewModelBuilder, LecturerIndexViewModelBuilder>();
        services.AddScoped<IStudentIndexViewModelBuilder, StudentIndexViewModelBuilder>();
    }
}
