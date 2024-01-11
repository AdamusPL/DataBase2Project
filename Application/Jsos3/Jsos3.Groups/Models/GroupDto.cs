namespace Jsos3.Groups.Models;

public class GroupDto
{
    public required string Lecturer { get; set; }
    public required string DayOfTheWeek { get; set; }
    public required string StartTime { get; set; }
    public required string EndTime { get; set; }
    public required string Type { get; set; }
}
