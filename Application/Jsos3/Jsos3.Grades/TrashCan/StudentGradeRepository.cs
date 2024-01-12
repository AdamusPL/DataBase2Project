using Jsos3.Shared.Db;
using Dapper;

namespace Jsos3.Grades.TrashCan;

internal class StudentGradeRepository
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
    g.Text AS [{nameof(StudentGrade.Text)}],
FROM [dbo].[Grade] g
WHERE g.StudentInGroupId = (SELECT Id FROM Student_Group
WHERE StudentId = @studentId and GroupId LIKE @groupId)
";
        var queryResult = await connection.QueryAsync<StudentGrade>(query, new { studentId, groupId });

        return queryResult.ToList();
    }
}
