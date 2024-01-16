namespace Jsos3.Groups.Infrastructure.Models;

internal readonly record struct StudentGroup(
    string Id,
    string SemesterId,
    int DayOfTheWeek,
    TimeSpan StartTime,
    TimeSpan EndTime,
    int Capacity,
    int Regularity,
    string Course,
    int Type,
    string LecturerName,
    string LecturerSurname,
    string CourseLecturerName,
    string CourseLecturerSurname,
    int Ects,
    int CourseId);
