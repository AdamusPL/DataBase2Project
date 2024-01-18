namespace Jsos3.Groups.Models;

public readonly record struct CourseData(
    int Id,
    string Name,
    int Ects,
    string Lecturer,
    decimal? Grade);
