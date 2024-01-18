namespace Jsos3.Groups.Models;

public readonly record struct StudentCourseKey(
    int Id,
    string Name,
    int Ects,
    string Lecturer,
    decimal? Grade);
