using Jsos3.LecturerInformations.Infrastructure.Repository;
using Jsos3.LecturerInformations.Models;

namespace Jsos3.LecturerInformations.Services;

public interface ILecturerService
{
    Task<List<LecturerDataDto>> GetLecturersAtPage(string? searchTerm, int pageIndex);
    Task<int> GetLecturersPagesCount(string? searchTerm);
}

internal class LecturerService : ILecturerService
{
    private readonly ILecturersDataRepository _lecturersDataRepository;

    private const int PageSize = 10;

    public LecturerService(ILecturersDataRepository lecturersDataRepository)
    {
        _lecturersDataRepository = lecturersDataRepository;
    }

    public async Task<List<LecturerDataDto>> GetLecturersAtPage(string? searchTerm, int pageIndex)
    {
        var lecturers = await _lecturersDataRepository.GetPagedLecturers(searchTerm, pageIndex * PageSize, PageSize);
        var lecturerIds = lecturers.Select(x => x.Id);

        var phones = await _lecturersDataRepository.GetLecturersPhones(lecturerIds);
        var emails = await _lecturersDataRepository.GetLecturersEmails(lecturerIds);

        return lecturers
            .Select(x => new LecturerDataDto
            {
                Name = x.Name,
                Surname = x.Surname,
                Phones = phones.GetValueOrDefault(x.Id, []),
                Emails = emails.GetValueOrDefault(x.Id, [])
            }).ToList();
    }

    public async Task<int> GetLecturersPagesCount(string? searchTerm)
    { 
        var result = (decimal)await _lecturersDataRepository.GetLecturersPagesCount(searchTerm) / PageSize;
        return (int)Math.Ceiling(result);
    }
}
