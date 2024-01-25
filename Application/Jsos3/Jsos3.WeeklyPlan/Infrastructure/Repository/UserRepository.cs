using Dapper;
using Jsos3.Shared.Db;

namespace Jsos3.WeeklyPlan.Infrastructure.Repository;

internal interface IUserRepository
{
    Task<int> GetUserStudentId(int userId);
    Task<int> GetUserLecturerId(int userId);
}

internal class UserRepository : IUserRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public UserRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<int> GetUserLecturerId(int userId)
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();

        var query = @"
SELECT l.Id
FROM [dbo].[Lecturer] l
INNER JOIN [dbo].[User] u ON u.Id = l.UserId
WHERE u.Id = @UserId";

        return await connection.ExecuteScalarAsync<int>(query, new { userId });
    }

    public async Task<int> GetUserStudentId(int userId)
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();

        var query = @"
SELECT s.Id
FROM [dbo].[Student] s
INNER JOIN [dbo].[User] u ON u.Id = s.UserId
WHERE u.Id = @UserId";

        return await connection.ExecuteScalarAsync<int>(query, new { userId });
    }
}
