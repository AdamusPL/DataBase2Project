using Dapper;
using Jsos3.Grades.Infrastructure.Models;
using Jsos3.Shared.Db;

namespace Jsos3.Grades.Infrastructure.Repository;

internal interface IGradeRepository
{
    Task AddGrade(int studentInGroupId, string gradeText, decimal grade, bool isFinal);
    Task<Dictionary<int, List<StudentGrade>>> GetStudentsGrades(string groupId, IEnumerable<int> studentIds);
    Task AcceptGrade(int gradeId);
    Task DeclineGrade(int gradeId);
}

internal class GradeRepository : IGradeRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public GradeRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task AcceptGrade(int gradeId)
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();
        var query = @$"
UPDATE [dbo].[Grade]
set Accepted = 1
WHERE Id = @gradeId AND Accepted IS NULL
";
        await connection.ExecuteAsync(query, new { gradeId });
    }

    public async Task AddGrade(int studentInGroupId, string gradeText, decimal grade, bool isFinal)
    {
        using var connection = await _dbConnectionFactory.GetOpenLecturerConnectionAsync();

        var query = @$"
INSERT INTO [dbo].[Grade] (Grade, IsFinal, StudentInGroupId, Text)
VALUES (@grade, @isFinal, @studentInGroupId, @gradeText)
";
        await connection.ExecuteAsync(query, new { studentInGroupId, gradeText, grade, isFinal });
    }

    public async Task DeclineGrade(int gradeId)
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();
        var query = @$"
UPDATE [dbo].[Grade]
set Accepted = 0
WHERE Id = @gradeId AND Accepted IS NULL
";
        await connection.ExecuteAsync(query, new { gradeId });
    }

    public async Task<Dictionary<int, List<StudentGrade>>> GetStudentsGrades(string groupId, IEnumerable<int> studentIds)
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();

        var query = @$"
SELECT
    g.Id AS [{nameof(StudentGrade.Id)}],
    g.Grade AS [{nameof(StudentGrade.Grade)}],
    g.Accepted AS [{nameof(StudentGrade.Accepted)}],
    g.IsFinal AS [{nameof(StudentGrade.IsFinal)}],
    g.Text AS [{nameof(StudentGrade.Text)}],
    sg.StudentId AS [{nameof(StudentGrade.StudentId)}]
FROM [dbo].[Grade] g
INNER JOIN [dbo].[Student_Group] sg ON g.StudentInGroupId = sg.Id
WHERE sg.GroupId = @groupId AND sg.StudentId IN @studentIds
";
        var queryResult = await connection.QueryAsync<StudentGrade>(query, new { studentIds, groupId });

        return queryResult
            .GroupBy(x => x.StudentId)
            .ToDictionary(x => x.Key, x => x.ToList());
    }
}
