using Dapper;
using Jsos3.Absences.Helpers;
using Jsos3.Absences.Infrastructure.Models;
using Jsos3.Shared.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Jsos3.Absences.Infrastructure.Repository;

internal interface IUpdatePresenceOfStudentRepository
{
    Task AddPresence(AbsencePageDto absencePageDto);
}

internal class UpdatePresenceOfStudentRepository : IUpdatePresenceOfStudentRepository
{
    public UpdatePresenceOfStudentRepository(IDbConnectionFactory dbConnectionFactory) 
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    private readonly IDbConnectionFactory _dbConnectionFactory;

    public async Task AddPresence(AbsencePageDto absencePageDto)
    {
        using var connection = await _dbConnectionFactory.GetOpenLecturerConnectionAsync();

        var query = $@"

SELECT TOP 1 
Id 
FROM Student_Group
WHERE StudentId = @StudentId AND GroupId LIKE @GroupId

";
        var studentInGroupId = await connection.QueryFirstOrDefaultAsync<int>(query, new { absencePageDto.StudentId, absencePageDto.GroupId });


        if (!absencePageDto.IsChecked)
        {
            query = $@" 

INSERT INTO Absence (Absence.Date, StudentInGroupId) VALUES (@Date, @studentInGroupId);

";
        }

        else
        {
            query = $@" 

DELETE FROM Absence WHERE StudentInGroupId = @studentInGroupId AND Date = @Date;

";
        }

        var parameters = new DynamicParameters();
        parameters.Add("Date", absencePageDto.Date.Date.ToString("yyyy-MM-dd"));
        parameters.Add("StudentInGroupId", studentInGroupId);

        try
        {
            await connection.ExecuteAsync(query, parameters);
        }
        catch (Exception ex)
        {
            // Log or handle the exception
            Console.WriteLine(ex.Message);
        }

    }
}
