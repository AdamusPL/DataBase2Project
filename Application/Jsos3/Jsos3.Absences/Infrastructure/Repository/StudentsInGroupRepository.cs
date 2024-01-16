using Dapper;
using Jsos3.Absences.Infrastructure.Models;
using Jsos3.Shared.Db;

namespace Jsos3.Absences.Infrastructure.Repository;

internal interface IStudentsInGroupRepository
{
    Task<List<StudentInGroup>> GetSortedStudentsFromGroup(string groupId);
    Task<int> GetStudentInGroupId(int studentId, string groupId);
}

internal class StudentsInGroupRepository : IStudentsInGroupRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public StudentsInGroupRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<List<StudentInGroup>> GetSortedStudentsFromGroup(string groupId)
    {
        using var connection = await _dbConnectionFactory.GetOpenLecturerConnectionAsync();

        var query = @$"
SELECT 
sg.Id AS [{nameof(StudentInGroup.StudentInGroupId)}],
g.Id AS [{nameof(StudentInGroup.GroupId)}],
s.Id AS [{nameof(StudentInGroup.StudentId)}],
u.Name AS [{nameof(StudentInGroup.Name)}],
u.Surname AS [{nameof(StudentInGroup.Surname)}]
FROM [Group] g
INNER JOIN [Student_Group] sg ON g.Id = sg.GroupId
INNER JOIN [Student] s ON sg.StudentId = s.Id
INNER JOIN [User] u ON u.Id = s.UserId
WHERE g.Id LIKE @groupId
ORDER BY u.Surname ASC
    
";
        var queryResult = await connection.QueryAsync<StudentInGroup>(query, new { groupId });

        return queryResult.ToList();
    }

    public async Task<int> GetStudentInGroupId(int studentId, string groupId)
    {
        using var connection = await _dbConnectionFactory.GetOpenLecturerConnectionAsync();

        var query = $@"

SELECT TOP 1 
Id 
FROM Student_Group
WHERE StudentId = @studentId AND GroupId LIKE @groupId

";
        return await connection.QueryFirstOrDefaultAsync<int>(query, new { studentId, groupId });
    }
}
