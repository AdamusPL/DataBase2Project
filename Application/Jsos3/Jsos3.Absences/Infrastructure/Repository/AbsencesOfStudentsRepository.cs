using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jsos3.Shared.Db;
using Dapper;
using Jsos3.Absences.Infrastructure.Models;


namespace Jsos3.Absences.Infrastructure.Repository;

public interface IAbsencesOfStudentsRepository
{
    Task<List<AbsenceOfStudents>> GetAbsencesOfStudentsInGroup(string groupId);
}

internal class AbsencesOfStudentsRepository : IAbsencesOfStudentsRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public AbsencesOfStudentsRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    //connection with DB, query with absences and take results to List
    public async Task<List<AbsenceOfStudents>> GetAbsencesOfStudentsInGroup(string groupId)
    {
        using var connection = await _dbConnectionFactory.GetOpenLecturerConnectionAsync();

        var query = @$"
SELECT
    sg.GroupId AS [{nameof(AbsenceOfStudents.GroupId)}],
    s.Id AS [{nameof(AbsenceOfStudents.StudentId)}],
    u.Name AS [{nameof(AbsenceOfStudents.Name)}],
    u.Surname AS [{nameof(AbsenceOfStudents.Surname)}],
    a.Date AS [{nameof(AbsenceOfStudents.Date)}]
FROM [Student_Group] sg
INNER JOIN [Student] s ON sg.StudentId = s.Id
INNER JOIN [User] u ON s.UserId = u.Id
INNER JOIN [Absence] a ON a.StudentInGroupId = sg.Id
WHERE sg.GroupId LIKE @groupId
";
        var queryResult = await connection.QueryAsync<AbsenceOfStudents>(query, new { groupId });

        return queryResult.ToList();
    }
}
