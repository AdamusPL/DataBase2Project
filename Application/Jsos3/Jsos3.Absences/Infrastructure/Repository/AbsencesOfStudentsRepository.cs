using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jsos3.Shared.Db;
using Dapper;
using Jsos3.Absences.Infrastructure.Models;


namespace Jsos3.Absences.Infrastructure.Repository;

internal interface IAbsencesOfStudentsRepository
{
    Task<List<AbsenceOfStudent>> GetAbsencesOfStudentsInGroup(string groupId);
}

internal class AbsencesOfStudentsRepository : IAbsencesOfStudentsRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public AbsencesOfStudentsRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<List<AbsenceOfStudent>> GetAbsencesOfStudentsInGroup(string groupId)
    {
        using var connection = await _dbConnectionFactory.GetOpenLecturerConnectionAsync();

        var query = @$"
SELECT
    sg.GroupId AS [{nameof(AbsenceOfStudent.GroupId)}],
    s.Id AS [{nameof(AbsenceOfStudent.StudentId)}],
    u.Name AS [{nameof(AbsenceOfStudent.Name)}],
    u.Surname AS [{nameof(AbsenceOfStudent.Surname)}],
    a.Date AS [{nameof(AbsenceOfStudent.Date)}]
FROM [Student_Group] sg
INNER JOIN [Student] s ON sg.StudentId = s.Id
INNER JOIN [User] u ON s.UserId = u.Id
INNER JOIN [Absence] a ON a.StudentInGroupId = sg.Id
WHERE sg.GroupId LIKE @groupId
";
        var queryResult = await connection.QueryAsync<AbsenceOfStudent>(query, new { groupId });

        return queryResult.ToList();
    }
}
