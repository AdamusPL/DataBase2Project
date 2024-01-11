using Dapper;
using Jsos3.Absences.Infrastructure.Models;
using Jsos3.Shared.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsos3.Absences.Infrastructure.Repository;

public interface IStudentsInGroupRepository
{
    Task<List<StudentInGroup>> GetStudentsFromGroup(string groupId);
}

internal class StudentsInGroupRepository : IStudentsInGroupRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public StudentsInGroupRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<List<StudentInGroup>> GetStudentsFromGroup(string groupId)
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
    
";
        var queryResult = await connection.QueryAsync<StudentInGroup>(query, new { groupId });

        return queryResult.ToList();
    }
}
