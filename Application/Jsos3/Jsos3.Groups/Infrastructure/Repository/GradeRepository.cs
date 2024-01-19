using Dapper;
using Jsos3.Groups.Infrastructure.Models;
using Jsos3.Shared.Db;
using Jsos3.Shared.Models;

namespace Jsos3.Groups.Infrastructure.Repository;

internal interface IGradeRepository
{
    Task<Dictionary<int, Grade>> GetStudentCoursesGrades(int studentId, string semesterId);
    Task<decimal> GetAverageGrade(int studentId, string semesterId);
}

internal class GradeRepository : IGradeRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public GradeRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<decimal> GetAverageGrade(int studentId, string semesterId)
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();

        var query = @"
EXEC [dbo].[GetStudentWeightedAverageGradeInSemester]
    @StudentId = @StudentId,
    @SemesterId = @SemesterId
";

        return await connection.ExecuteScalarAsync<decimal>(query, new { studentId, semesterId });
    }

    public async Task<Dictionary<int, Grade>> GetStudentCoursesGrades(int studentId, string semesterId)
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();

        var query = @$"
SELECT
    c.Id AS [{nameof(CourseGrade.CourseId)}],
    gr.Grade AS [{nameof(CourseGrade.Value)}],
    gr.Accepted AS [{nameof(CourseGrade.Accepted)}]
FROM [dbo].[Course] c
INNER JOIN [dbo].[Group] g ON g.CourseId = c.Id
INNER JOIN [dbo].[Student_Group] sg ON sg.GroupId = g.Id
INNER JOIN [dbo].[Grade] gr ON gr.StudentInGroupId = sg.Id
WHERE gr.IsFinal = 1
        AND g.TypeId = {(int)GroupType.Lecture}
        AND sg.StudentId = @StudentId
        AND g.SemesterId LIKE @SemesterId
";

        var queryResult = await connection.QueryAsync<CourseGrade>(query, new { studentId, semesterId });

        return queryResult
            .GroupBy(x => x.CourseId)
            .ToDictionary(x => x.Key, x => x.Select(x => new Grade(x.Value, x.Accepted)).Last());
    }
}
