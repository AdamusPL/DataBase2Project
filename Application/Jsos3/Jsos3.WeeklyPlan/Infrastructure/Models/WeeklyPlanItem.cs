using Jsos3.Shared.Models;

namespace Jsos3.WeeklyPlan.Infrastructure.Models;

internal readonly record struct WeeklyPlanItem(
    DateTime Date,
    Regularity RegularityId,
    string Course,
    GroupType GroupTypeId,
    TimeSpan StartTime,
    TimeSpan EndTime,
    string Classroom,
    string Lecturer);
