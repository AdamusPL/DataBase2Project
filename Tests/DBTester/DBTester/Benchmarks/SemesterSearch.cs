using Dapper;
using Models;
using Shared;
using System.Data;

namespace DBTester.Benchmarks;

public class SemesterSearch : BenchmarkBase
{
    private const int _numberOfSemesters = 1_000_000;
    private const int BatchSize = 2000;
    private readonly DateTime _now = new(2023, 12, 21);
    private readonly DateTime _oldTime = new(1753, 1, 1);

    public SemesterSearch()
        : base(DBConnectionProvider.SuperAdminConnection())
    { }

    protected override async Task Benchmark(IDbTransaction transaction)
    {
        await RunTest("Search added first", async () =>
        {
            var foundEntity = await _dbConnection.QueryFirstOrDefaultAsync<Semester>(@"
SELECT *
FROM Semester
WHERE StartDate = @StartDate
", new { StartDate = _oldTime }, transaction);
        }, 100);

        await RunTest("Search added last", async () =>
        {
            var foundEntity = await _dbConnection.QueryFirstOrDefaultAsync<Semester>(@"
SELECT *
FROM Semester
WHERE StartDate = @StartDate
", new { StartDate = _now }, transaction);
        }, 100);
    }

    protected override async Task PrepareData(IDbTransaction transaction)
    {
        var rnd = new Random();

        await Insert(
            new { Id = RandomId, StartDate = _oldTime, EndDate = _oldTime },
            transaction);

        var semesters = new List<Semester>();
        for (int i = 0; i < _numberOfSemesters; ++i)
        {
            var year = rnd.Next(2000, 2020);
            var month = rnd.Next(1, 12);

            semesters.Add(new Semester
            {
                Id = RandomId,
                StartDate = new DateTime(year, month, 1),
                EndDate = new DateTime(year, month, 12)
            });

            if ((i + 1) % BatchSize == 0)
            {
                Console.WriteLine($"Inserting {(i + 1) / BatchSize}/{_numberOfSemesters / BatchSize}");

                await Insert(
                    semesters,
                    transaction);
                semesters.Clear();
            }
        }

        await Insert(
            new { Id = RandomId, StartDate = _now, EndDate = _now },
            transaction);
    }

    private string RandomId =>
        Guid.NewGuid().ToString().Substring(0, 20);

    private Task Insert(object entity, IDbTransaction transaction) => _dbConnection.ExecuteAsync(@"
INSERT INTO dbo.Semester (Id, StartDate, EndDate)
VALUES (@Id, @StartDate, @EndDate)", entity, transaction);
}
