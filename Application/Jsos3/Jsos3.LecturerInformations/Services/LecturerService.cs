using AutoMapper;
using Jsos3.LecturerInformations.Helpers;
using Jsos3.LecturerInformations.Infrastructure.Models;
using Jsos3.LecturerInformations.Infrastructure.Repository;

namespace Jsos3.LecturerInformations.Services
{
    public interface ILecturerService
    {
        Task<List<LecturerDataDto>> GetAllLecturers();
    }

    internal class LecturerService : ILecturerService
    {
        private readonly ILecturersDataRepository _lecturersDataRepository;
        public LecturerService(ILecturersDataRepository lecturersDataRepository)
        {
            _lecturersDataRepository = lecturersDataRepository;
        }

        public async Task<List<LecturerDataDto>> GetAllLecturers()
        {
            var fromDatabase = await _lecturersDataRepository.GetAllLecturers();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<LecturerData, LecturerDataDto>());
            var mapper = new Mapper(config);

            return mapper.Map<List<LecturerDataDto>>(fromDatabase)
                .OrderBy(lecturer => lecturer.Surname)
                .ToList();
        }
    }
}
