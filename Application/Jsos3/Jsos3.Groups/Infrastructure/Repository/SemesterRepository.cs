using Dapper;
using Jsos3.Shared.Db;

namespace Jsos3.Groups.Infrastructure.Repository;

internal interface ISemesterRepository
{
    Task<string> GetLatestSemesterId();
    Task<List<string>> GetAllSemesterIds();
}

internal class SemesterRepository : ISemesterRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public SemesterRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<List<string>> GetAllSemesterIds()
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();
        var query = @"
SELECT Id
FROM [dbo].[Semester]
ORDER BY EndDate DESC, StartDate DESC";

        var queryResult = await connection.QueryAsync<string>(query);

        return queryResult.ToList();
    }

    public async Task<string> GetLatestSemesterId()
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();
        var query = @"
SELECT TOP 1 Id
FROM [dbo].[Semester]
ORDER BY EndDate DESC, StartDate DESC";

        return await connection.QuerySingleAsync<string>(query);
    }
}
