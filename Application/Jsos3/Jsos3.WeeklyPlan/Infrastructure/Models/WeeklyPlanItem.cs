using Jsos3.Shared.Models;

namespace Jsos3.WeeklyPlan.Infrastructure.Models;

internal record WeeklyPlanItem(
    DateTime Date,
    Regularity RegularityId,
    string Course,
    GroupType GroupType,
    TimeSpan StartTime,
    TimeSpan EndTime,
    string Classroom,
    string Lecturer);
