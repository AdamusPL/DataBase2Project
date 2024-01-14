using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Jsos3.LecturerInformations.Infrastructure.Models;
using Jsos3.Shared.Db;


namespace Jsos3.LecturerInformations.Infrastructure.Repository;

internal interface ILecturersDataRepository
{
    Task<List<LecturerData>> GetAllLecturers();
}

internal class LecturersDataRepository : ILecturersDataRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public LecturersDataRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<List<LecturerData>> GetAllLecturers()
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();

        var query = @$"
SELECT
Name AS [{nameof(LecturerData.Name)}], 
Surname AS [{nameof(LecturerData.Surname)}], 
Email AS [{nameof(LecturerData.Email)}], 
Phone AS [{nameof(LecturerData.Phone)}] 
FROM Lecturer
INNER JOIN [User] u ON Lecturer.UserId = u.Id
INNER JOIN Email ON u.Id = Email.UserId
INNER JOIN WorkPhone ON Lecturer.UserId = WorkPhone.UserId;
";
        var queryResult = await connection.QueryAsync<LecturerData>(query, new { });

        return queryResult.ToList();
    }
}
