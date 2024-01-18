using Jsos3.Shared.Models;

namespace Jsos3.Groups.Models;

public class GroupDto
{
    public required string Id { get; set; }
    public required string Lecturer { get; set; }
    public required DayOfWeek DayOfTheWeek { get; set; }
    public required string StartTime { get; set; }
    public required string EndTime { get; set; }
    public required GroupType Type { get; set; }
}
