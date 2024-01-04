using Dapper;
using Jsos3.Groups.Infrastructure.Models;
using Jsos3.Shared.Db;

namespace Jsos3.Groups.Infrastructure.Repository;

internal interface IStudentGroupRepository
{
    Task<List<StudentGroup>> GetStudentGroupsInSemester(int studentId, string semesterId);
}

internal class StudentGroupRepository : IStudentGroupRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public StudentGroupRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<List<StudentGroup>> GetStudentGroupsInSemester(int studentId, string semesterId)
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();

        var query = @$"
SELECT
    g.Id AS [{nameof(StudentGroup.Id)}],
    sem.Id AS [{nameof(StudentGroup.SemesterId)}],
    g.DayOfTheWeek AS [{nameof(StudentGroup.DayOfTheWeek)}],
    g.StartTime AS [{nameof(StudentGroup.StartTime)}],
    g.EndTime AS [{nameof(StudentGroup.EndTime)}],
    g.Capacity AS [{nameof(StudentGroup.Capacity)}],
    g.RegularityId AS [{nameof(StudentGroup.Regularity)}],
    c.Name AS [{nameof(StudentGroup.Course)}],
    g.TypeId AS [{nameof(StudentGroup.Type)}],
    u.Name AS [{nameof(StudentGroup.LecturerName)}],
    u.Surname AS [{nameof(StudentGroup.LecturerSurname)}],
    c.ECTS AS [{nameof(StudentGroup.Ects)}],
    c.Id AS [{nameof(StudentGroup.CourseId)}]
FROM [dbo].[Group] g
INNER JOIN [dbo].[Student_Group] sg ON sg.GroupId = g.Id
INNER JOIN [dbo].[Student] s ON s.Id = sg.StudentId
INNER JOIN [dbo].[Semester] sem ON sem.Id = g.SemesterId
INNER JOIN [dbo].[Course] c ON c.Id = g.CourseId
INNER JOIN [dbo].[Group_Lecturer] gl ON gl.GroupId = g.Id
INNER JOIN [dbo].[Lecturer] l ON l.Id = gl.LecturerId
INNER JOIN [dbo].[User] u ON u.Id = l.UserId
WHERE s.Id = @StudentId AND sem.Id LIKE @SemesterId
ORDER BY c.Name
";
        var queryResult = await connection.QueryAsync<StudentGroup>(query, new { studentId, semesterId });

        return queryResult.ToList();
    }
}
