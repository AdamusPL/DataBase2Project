using Dapper;
using Jsos3.Shared.Db;
using Jsos3.WeeklyPlan.Infrastructure.Models;

namespace Jsos3.WeeklyPlan.Infrastructure.Repository;

internal interface IWeeklyPlanRepository
{
    Task<List<WeeklyPlanItem>> GetStudentWeeklyPlan(int studentId, DateTime start, DateTime end);
}

internal class WeeklyPlanRepository : IWeeklyPlanRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public WeeklyPlanRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<List<WeeklyPlanItem>> GetStudentWeeklyPlan(int studentId, DateTime start, DateTime end)
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();

        var query = "EXEC [dbo].[GetStudentWeeklyPlan] @StudentId, @StartDate, @EndDate";

        var result = await connection.QueryAsync<WeeklyPlanItem>(query, new
        {
            StudentId = studentId,
            StartDate = start,
            EndDate = end
        });

        return result.ToList();
    }
}
