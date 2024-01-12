using Jsos3.Groups.Infrastructure.Repository;

namespace Jsos3.Groups.Services;

public interface ISemesterService
{
    Task<List<string>> GetSemesters();
}

internal class SemesterService : ISemesterService
{
    private readonly ISemesterRepository _semesterRepository;

    public SemesterService(ISemesterRepository semesterRepository)
    {
        _semesterRepository = semesterRepository;
    }

    public async Task<List<string>> GetSemesters()
    {
        return await _semesterRepository.GetAllSemesterIds();
    }
}
