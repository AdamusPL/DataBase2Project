using Dapper;
using Jsos3.Groups.Infrastructure.Models;
using Jsos3.Shared.Db;

namespace Jsos3.Groups.Infrastructure.Repository;

internal interface IGroupRepository
{
    Task<List<Group>> GetStudentGroupsInSemester(int studentId, string semesterId);
    Task<List<Group>> GetLecturerGroupsInSemester(int lecturerId, string semesterId);
}

internal class GroupRepository : IGroupRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public GroupRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<List<Group>> GetLecturerGroupsInSemester(int lecturerId, string semesterId)
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();

        var query = @$"
{BaseQueryForGroups}
WHERE u.Id = @LecturerId AND sem.Id LIKE @SemesterId
ORDER BY c.Name
";
        var queryResult = await connection.QueryAsync<Group>(query, new { lecturerId, semesterId });

        return queryResult.ToList();
    }

    public async Task<List<Group>> GetStudentGroupsInSemester(int studentId, string semesterId)
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();

        var query = @$"
{BaseQueryForGroups}
INNER JOIN [dbo].[Student_Group] sg ON sg.GroupId = g.Id
INNER JOIN [dbo].[Student] s ON s.Id = sg.StudentId
INNER JOIN [dbo].[User] su ON su.Id = s.UserId
WHERE su.Id = @StudentId AND sem.Id LIKE @SemesterId
ORDER BY c.Name
";
        var queryResult = await connection.QueryAsync<Group>(query, new { studentId, semesterId });

        return queryResult.ToList();
    }

    private const string BaseQueryForGroups = $@"
SELECT
    g.Id AS [{nameof(Group.Id)}],
    sem.Id AS [{nameof(Group.SemesterId)}],
    g.DayOfTheWeek AS [{nameof(Group.DayOfTheWeek)}],
    g.StartTime AS [{nameof(Group.StartTime)}],
    g.EndTime AS [{nameof(Group.EndTime)}],
    g.Capacity AS [{nameof(Group.Capacity)}],
    g.RegularityId AS [{nameof(Group.Regularity)}],
    c.Name AS [{nameof(Group.Course)}],
    g.TypeId AS [{nameof(Group.Type)}],
    u.Name AS [{nameof(Group.LecturerName)}],
    u.Surname AS [{nameof(Group.LecturerSurname)}],
    c.ECTS AS [{nameof(Group.Ects)}],
    clu.Name AS [{nameof(Group.CourseLecturerName)}],
    clu.Surname AS [{nameof(Group.CourseLecturerSurname)}],
    c.Id AS [{nameof(Group.CourseId)}]
FROM [dbo].[Group] g
INNER JOIN [dbo].[Semester] sem ON sem.Id = g.SemesterId
INNER JOIN [dbo].[Course] c ON c.Id = g.CourseId
INNER JOIN [dbo].[Group_Lecturer] gl ON gl.GroupId = g.Id
INNER JOIN [dbo].[Lecturer] l ON l.Id = gl.LecturerId
INNER JOIN [dbo].[User] u ON u.Id = l.UserId
INNER JOIN [dbo].[Lecturer] cl ON cl.Id = c.LecturerId
INNER JOIN [dbo].[User] clu ON clu.Id = cl.UserId";
}
