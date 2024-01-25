using Jsos3.Groups.Infrastructure.Models;

namespace Jsos3.Groups.Models;

internal readonly record struct StudentCourseKey(
    int Id,
    string Name,
    int Ects,
    string Lecturer,
    decimal? Grade,
    bool? GradeAccepted);
