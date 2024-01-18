using Dapper;
using Jsos3.Shared.Db;

namespace Jsos3.Groups.Infrastructure.Repository;

internal interface ISemesterRepository
{
    Task<string> GetLatestSemesterId();
    Task<List<string>> GetAllSemesterIds();
    Task<int> GetStudentAllEcts(int studentId, string semesterId);
    Task<int> GetStudentReceivedEcts(int studentId, string semesterId, int gradeRequiredToPass);
}

internal class SemesterRepository : ISemesterRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public SemesterRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<List<string>> GetAllSemesterIds()
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();
        var query = @"
SELECT Id
FROM [dbo].[Semester]
ORDER BY EndDate DESC, StartDate DESC";

        var queryResult = await connection.QueryAsync<string>(query);

        return queryResult.ToList();
    }

    public async Task<string> GetLatestSemesterId()
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();
        var query = @"
SELECT TOP 1 Id
FROM [dbo].[Semester]
ORDER BY EndDate DESC, StartDate DESC";

        return await connection.QuerySingleAsync<string>(query);
    }

    public async Task<int> GetStudentAllEcts(int studentId, string semesterId)
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();
        var query = @"
SELECT SUM(c.ECTS)
FROM [Student] s
INNER JOIN [Student_Group] sg ON s.Id = sg.StudentId
INNER JOIN [Group] g ON sg.GroupId = g.Id
INNER JOIN [Course] c ON g.CourseId = c.Id
WHERE s.Id = @StudentId AND SemesterId LIKE @SemesterId
";

        return await connection.ExecuteScalarAsync<int>(query, new { studentId, semesterId });
    }

    public async Task<int> GetStudentReceivedEcts(int studentId, string semesterId, int gradeRequiredToPass)
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();
        var query = @"
SELECT SUM(c.ECTS)
FROM [Student] s
INNER JOIN [Student_Group] sg ON s.Id = sg.StudentId
INNER JOIN [Group] g ON sg.GroupId = g.Id
INNER JOIN [Course] c ON g.CourseId = c.Id
LEFT JOIN [Grade] gr ON sg.Id = gr.StudentInGroupId
WHERE s.Id = @StudentId
        AND SemesterId LIKE @SemesterId
        AND gr.isFinal = 1 AND gr.Grade >= @GradeRequiredToPass
";

        return await connection.ExecuteScalarAsync<int>(query, new { studentId, semesterId, gradeRequiredToPass });
    }
}
