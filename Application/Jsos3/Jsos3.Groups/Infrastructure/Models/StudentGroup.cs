namespace Jsos3.Groups.Infrastructure.Models;

internal class StudentGroup
{
    public required string Id { get; set; }
    public required string SemesterId { get; set; }
    public required int DayOfTheWeek { get; set; }
    public required TimeSpan StartTime { get; set; }
    public required TimeSpan EndTime { get; set; }
    public required int Capacity { get; set; }
    public required int Regularity { get; set; }
    public required string Course { get; set; }
    public required int Type { get; set; }
    public required string LecturerName { get; set; }
    public required string LecturerSurname { get; set; }
    public required int Ects { get; set; }
    public required int CourseId { get; set; }
}
