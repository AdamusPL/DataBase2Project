using Jsos3.Shared.Db;
using Jsos3.Shared.Extensions;
using Jsos3.WeeklyPlan.Infrastructure.Repository;
using Jsos3.WeeklyPlan.Logic;
using Jsos3.WeeklyPlan.Services;
using Jsos3.WeeklyPlan.ViewModels.Builders;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Jsos3.WeeklyPlan;

public static class WeeklyPlanModule
{
    public static void AddWeeklyPlanModule(this IServiceCollection services)
    {
        services.AddProxed<IWeeklyPlanRepository, CachedWeeklyPlanRepository, WeeklyPlanRepository>(
            (p, impl) => new(p.GetRequiredService<IMemoryCache>(), impl),
            p => new(p.GetRequiredService<IDbConnectionFactory>()));

        services.AddTransient<IWeekRangeCalculator, WeekRangeCalculator>();
        services.AddTransient<IWeekDataCalculator, WeekDataCalculator>();
        services.AddTransient<IPlanService, PlanService>();
        services.AddTransient<IWeeklyPlanIndexViewModelBuilder, WeeklyPlanIndexViewModelBuilder>();
    }
}
