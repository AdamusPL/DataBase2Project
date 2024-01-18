using Jsos3.Shared.Db;
using Dapper;
using System.Text.RegularExpressions;
using Jsos3.Grades.Models;

namespace Jsos3.Grades.Repository;

public interface IStudentGradeRepository
{
    Task<List<StudentGrade>> GetStudentGrade(int studentId, string groupId);
    Task AcceptGrade(int gradeId);
    Task DeclineGrade(int gradeId);
}

internal class StudentGradeRepository : IStudentGradeRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public StudentGradeRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }


    public async Task<List<StudentGrade>> GetStudentGrade(int studentId, string groupId)
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();

        var query = @$"
SELECT
    g.Id AS [{nameof(StudentGrade.Id)}],
    g.Grade AS [{nameof(StudentGrade.Grade)}],
    g.Accepted AS [{nameof(StudentGrade.Accepted)}],
    g.IsFinal AS [{nameof(StudentGrade.IsFinal)}],
    g.Text AS [{nameof(StudentGrade.Text)}]
FROM [dbo].[Grade] g
INNER JOIN [dbo].[Student_Group] sg ON g.StudentInGroupId = sg.Id
WHERE sg.GroupId = @groupId AND sg.StudentId = @studentId
";
        var queryResult = await connection.QueryAsync<StudentGrade>(query, new { studentId, groupId });

        return queryResult.ToList();
    }

    public async Task AcceptGrade(int gradeId)
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();
        var query = @$"
UPDATE [dbo].[Grade]
set Accepted = 1
WHERE Id = @gradeId AND Accepted IS NULL
";
        await connection.ExecuteAsync(query, new { gradeId }); ;
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
}
