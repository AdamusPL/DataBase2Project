using Dapper;
using Jsos3.Grades.Infrastructure.Models;
using Jsos3.Shared.Db;

namespace Jsos3.Grades.Infrastructure.Repository;

internal interface IStudentRepository
{
    Task<List<Student>> GetStudents(string groupId);
    Task<int> GetStudentInGroupId(string groupId, int studentId);
    Task<int> GetUserStudentId(int userId);
}

internal class StudentRepository : IStudentRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public StudentRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<int> GetStudentInGroupId(string groupId, int studentId)
    {
        using var connection = await _dbConnectionFactory.GetOpenLecturerConnectionAsync();

        var query = @$"
SELECT Id
FROM [dbo].[Student_Group]
WHERE GroupId = @groupId AND StudentId = @studentId
";

        return await connection.ExecuteScalarAsync<int>(query, new { groupId, studentId });
    }

    public async Task<List<Student>> GetStudents(string groupId)
    {
        using var connection = await _dbConnectionFactory.GetOpenLecturerConnectionAsync();

        var query = @$"
SELECT
    s.Id AS [{nameof(Student.Id)}],
    u.Name AS [{nameof(Student.FirstName)}],
    u.Surname AS [{nameof(Student.LastName)}]
FROM [dbo].[Student] s
INNER JOIN [dbo].[User] u ON s.UserId = u.Id
INNER JOIN [dbo].[Student_Group] sg ON s.Id = sg.StudentId
WHERE sg.GroupId = @groupId
";
        var queryResult = await connection.QueryAsync<Student>(query, new { groupId });

        return queryResult.ToList();
    }

    public async Task<int> GetUserStudentId(int userId)
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();

        var query = @"
SELECT s.Id
FROM [dbo].[Student] s
INNER JOIN [dbo].[User] u ON u.Id = s.UserId
WHERE u.Id = @UserId";

        return await connection.ExecuteScalarAsync<int>(query, new { userId });
    }
}
