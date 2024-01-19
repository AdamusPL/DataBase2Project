using Dapper;
using Jsos3.Shared.Db;
using Jsos3.WeeklyPlan.Infrastructure.Models;

namespace Jsos3.WeeklyPlan.Infrastructure.Repository;

internal interface IWeeklyPlanRepository
{
    Task<List<WeeklyPlanItem>> GetStudentWeeklyPlan(int studentId, DateTime startDate, DateTime endDate);
    Task<List<WeeklyPlanItem>> GetLecturerWeeklyPlan(int lecturerId, DateTime startDate, DateTime endDate);
}

internal class WeeklyPlanRepository : IWeeklyPlanRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public WeeklyPlanRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<List<WeeklyPlanItem>> GetLecturerWeeklyPlan(int lecturerId, DateTime startDate, DateTime endDate)
    {
        using var connection = await _dbConnectionFactory.GetOpenLecturerConnectionAsync();

        var query = "EXEC [dbo].[GetLecturerWeeklyPlan] @LecturerId, @StartDate, @EndDate";

        var result = await connection.QueryAsync<WeeklyPlanItem>(query, new
        {
            lecturerId,
            startDate,
            endDate
        });

        return result.ToList();
    }

    public async Task<List<WeeklyPlanItem>> GetStudentWeeklyPlan(int studentId, DateTime startDate, DateTime endDate)
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();
        
        var query = "EXEC [dbo].[GetStudentWeeklyPlan] @StudentId, @StartDate, @EndDate";

        var result = await connection.QueryAsync<WeeklyPlanItem>(query, new
        {
            studentId,
            startDate,
            endDate
        });

        return result.ToList();
    }
}
