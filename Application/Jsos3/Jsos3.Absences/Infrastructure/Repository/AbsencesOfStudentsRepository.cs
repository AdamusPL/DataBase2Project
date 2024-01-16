using Dapper;
using Jsos3.Absences.Infrastructure.Models;
using Jsos3.Shared.Db;


namespace Jsos3.Absences.Infrastructure.Repository;

internal interface IAbsencesOfStudentsRepository
{
    Task<List<AbsenceOfStudent>> GetAbsencesOfStudentsInGroup(string groupId);
    Task AddPresence(int studentInGroupId, DateTime date);
    Task DeletePresence(int studentInGroupId, DateTime date);
}

internal class AbsencesOfStudentsRepository : IAbsencesOfStudentsRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public AbsencesOfStudentsRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task DeletePresence(int studentInGroupId, DateTime date)
    {
        using var connection = await _dbConnectionFactory.GetOpenLecturerConnectionAsync();

        var query = $@" 
INSERT INTO Absence (Absence.Date, StudentInGroupId) VALUES (@Date, @studentInGroupId);
";

        var parameters = new DynamicParameters();
        parameters.Add("Date", date.Date.ToString("yyyy-MM-dd"));
        parameters.Add("StudentInGroupId", studentInGroupId);

        await connection.ExecuteAsync(query, parameters);

    }

    public async Task AddPresence(int studentInGroupId, DateTime date)
    {
        using var connection = await _dbConnectionFactory.GetOpenLecturerConnectionAsync();
        var query = $@" 
DELETE FROM Absence WHERE StudentInGroupId = @studentInGroupId AND Date = @Date;

";

        var parameters = new DynamicParameters();
        parameters.Add("Date", date.Date.ToString("yyyy-MM-dd"));
        parameters.Add("StudentInGroupId", studentInGroupId);

        await connection.ExecuteAsync(query, parameters);

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
