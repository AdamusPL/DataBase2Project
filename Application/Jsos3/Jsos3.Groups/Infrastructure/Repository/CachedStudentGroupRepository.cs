using Jsos3.Groups.Infrastructure.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Jsos3.Groups.Infrastructure.Repository;

internal class CachedStudentGroupRepository : IStudentGroupRepository
{
    private readonly IStudentGroupRepository _studentGroupRepository;
    private readonly IMemoryCache _cache;
    private readonly static TimeSpan CacheLifetime = TimeSpan.FromMinutes(30);

    public CachedStudentGroupRepository(IStudentGroupRepository studentGroupRepository, IMemoryCache cache)
    {
        _studentGroupRepository = studentGroupRepository;
        _cache = cache;
    }

    public async Task<List<StudentGroup>> GetStudentGroupsInSemester(int studentId, string semesterId)
    {
        var result = await _cache.GetOrCreateAsync(
            $"{nameof(GetStudentGroupsInSemester)}_{studentId}_{semesterId}",
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = CacheLifetime;
                return await _studentGroupRepository.GetStudentGroupsInSemester(studentId, semesterId);
            });

        return result is null ? throw new ArgumentNullException() : result;
    }
}
