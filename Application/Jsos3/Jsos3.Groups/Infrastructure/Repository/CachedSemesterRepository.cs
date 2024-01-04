using Microsoft.Extensions.Caching.Memory;

namespace Jsos3.Groups.Infrastructure.Repository;

internal class CachedSemesterRepository : ISemesterRepository
{
    private readonly ISemesterRepository _semesterRepository;
    private readonly IMemoryCache _cache;

    private readonly static TimeSpan CacheLifetime = TimeSpan.FromMinutes(30);

    public CachedSemesterRepository(ISemesterRepository semesterRepository, IMemoryCache cache)
    {
        _semesterRepository = semesterRepository;
        _cache = cache;
    }

    public async Task<List<string>> GetAllSemesterIds()
    {
        var result = await _cache.GetOrCreateAsync(
            nameof(GetAllSemesterIds),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = CacheLifetime;
                return await _semesterRepository.GetAllSemesterIds();
            });

        return result ?? throw new ArgumentNullException();
    }

    public async Task<string> GetLatestSemesterId()
    {
        var result = await _cache.GetOrCreateAsync(
            nameof(GetLatestSemesterId),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = CacheLifetime;
                return await _semesterRepository.GetLatestSemesterId();
            });

        return result ?? throw new ArgumentNullException();
    }
}
