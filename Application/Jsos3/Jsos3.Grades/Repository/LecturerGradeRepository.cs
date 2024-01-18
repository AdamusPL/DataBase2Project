using Dapper;
using Jsos3.Grades.Models;
using Jsos3.Shared.Db;

namespace Jsos3.Grades.Repository;

public interface ILecturerGradePerository
{
    Task<List<Student>> GetStudents(string groupId);
}
internal class LecturerGradeRepository : ILecturerGradePerository
{

    private readonly IDbConnectionFactory _dbConnectionFactory;

    public LecturerGradeRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    public async Task<List<Student>> GetStudents(string groupId)
    {
        using var connection = await _dbConnectionFactory.GetOpenLecturerConnectionAsync();

        var query = @$"
SELECT
    s.Id AS [{nameof(Student.Id)}],
    u.Name AS [{nameof(Student.FirstName)}],
    u.Surname AS [{nameof(Student.LastName)}],
FROM [dbo].[Student] s
INNER JOIN [dbo].[User] u ON s.UserId = u.Id
INNER JOIN [dbo].[Student_Group] sg ON s.Id = sg.StudentId
WHERE sg.GroupId = @groupId
";
        var queryResult = await connection.QueryAsync<Student>(query, new { groupId });

        return queryResult.ToList();
    }
}
