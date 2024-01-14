using Jsos3.WeeklyPlan.Infrastructure.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Jsos3.WeeklyPlan.Infrastructure.Repository;

internal class CachedWeeklyPlanRepository : IWeeklyPlanRepository
{
    private readonly IMemoryCache _memoryCache;
    private readonly IWeeklyPlanRepository _weeklyPlanRepository;

    private static readonly TimeSpan CacheLifetime = TimeSpan.FromMinutes(30);

    public CachedWeeklyPlanRepository(IMemoryCache memoryCache, IWeeklyPlanRepository weeklyPlanRepository)
    {
        _memoryCache = memoryCache;
        _weeklyPlanRepository = weeklyPlanRepository;
    }

    public async Task<List<WeeklyPlanItem>> GetStudentWeeklyPlan(int studentId, DateTime start, DateTime end)
    {
        var cacheKey = $"{nameof(GetStudentWeeklyPlan)}_{studentId}_{start}_{end}";

        var result = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheLifetime;
            return await _weeklyPlanRepository.GetStudentWeeklyPlan(studentId, start, end);
        });

        return result is null ? throw new ArgumentNullException() : result;
    }

    public async Task<List<WeeklyPlanItem>> GetLecturerWeeklyPlan(int lecturerId, DateTime start, DateTime end)
    {
        var cacheKey = $"{nameof(GetLecturerWeeklyPlan)}_{lecturerId}_{start}_{end}";

        var result = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheLifetime;
            return await _weeklyPlanRepository.GetLecturerWeeklyPlan(lecturerId, start, end);
        });

        return result is null ? throw new ArgumentNullException() : result;
    }
}
