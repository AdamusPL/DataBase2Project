using Jsos3.Shared.Logic;
using Jsos3.WeeklyPlan.Infrastructure.Repository;
using Jsos3.WeeklyPlan.Models;

namespace Jsos3.WeeklyPlan.Services;

public interface IPlanService
{
    Task<Dictionary<DateTime, List<WeeklyPlanDto>>> GetStudentPlan(int studentId, DateTime startOfWeek, DateTime endOfWeek);
}

internal class PlanService : IPlanService
{
    private readonly IWeeklyPlanRepository _weeklyPlanRepository;
    private readonly ITranslationService _translationService;

    public PlanService(IWeeklyPlanRepository weeklyPlanRepository, ITranslationService translationService)
    {
        _weeklyPlanRepository = weeklyPlanRepository;
        _translationService = translationService;
    }

    public async Task<Dictionary<DateTime, List<WeeklyPlanDto>>> GetStudentPlan(int studentId, DateTime startOfWeek, DateTime endOfWeek)
    {
        var weeklyPlan = await _weeklyPlanRepository.GetStudentWeeklyPlan(studentId, startOfWeek, endOfWeek);

        return weeklyPlan
            .Select(x => new WeeklyPlanDto
            {
                Date = x.Date,
                Regularity = _translationService.Translate(x.RegularityId),
                Course = x.Course,
                GroupType = _translationService.Translate(x.GroupType),
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Classroom = x.Classroom,
                Lecturer = x.Lecturer
            })
            .GroupBy(x => x.Date)
            .ToDictionary(x => x.Key, x => x.ToList());
    }
}
