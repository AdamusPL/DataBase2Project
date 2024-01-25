using Dapper;
using Jsos3.Shared.Db;

namespace Jsos3.Groups.Infrastructure.Repository;

internal interface IStudentRepository
{
    Task<int> GetUserStudentId(int userId);
}

internal class StudentRepository : IStudentRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public StudentRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
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
