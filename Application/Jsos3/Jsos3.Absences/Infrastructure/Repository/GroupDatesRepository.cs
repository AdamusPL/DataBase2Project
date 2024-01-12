using Dapper;
using Jsos3.Absences.Infrastructure.Models;
using Jsos3.Shared.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsos3.Absences.Infrastructure.Repository;

public interface IGroupDatesRepository
{
    Task<GroupDate> GetDatesOfGroup(string groupId);
}

internal class GroupDatesRepository : IGroupDatesRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public GroupDatesRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<GroupDate> GetDatesOfGroup(string groupId)
    {
        using var connection = await _dbConnectionFactory.GetOpenLecturerConnectionAsync();

        var query = @$"
SELECT 
g.Id AS [{nameof(GroupDate.GroupId)}], 
s.StartDate AS [{nameof(GroupDate.Start)}], 
s.EndDate AS [{nameof(GroupDate.End)}], 
g.DayOfTheWeek AS [{nameof(GroupDate.DayOfTheWeek)}], 
r.Id AS [{nameof(GroupDate.Regularity)}]
FROM [Group] g
INNER JOIN [Regularity] r ON g.RegularityId = r.Id
INNER JOIN [Semester] s ON s.Id = g.SemesterId
WHERE g.Id LIKE @groupId;
    
";
        var queryResult = await connection.QueryFirstOrDefaultAsync<GroupDate>(query, new { groupId });

        return queryResult;
    }
}
