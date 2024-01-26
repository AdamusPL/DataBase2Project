using Jsos3.Grades.Infrastructure.Models;
using Jsos3.Grades.Models;
using Jsos3.Shared.Models;

namespace Jsos3.Grades.Helpers;

internal static class GradeDtoMapper
{
    internal static StudentGradeDto ToDto(this StudentGrade grade) =>
        new()
        {
            Id = grade.Id,
            Text = grade.Text,
            Grade = grade.Grade,
            IsFinal = grade.IsFinal ? GradeType.Final : GradeType.Partial,
            Accepted = grade.Accepted
        };
}
