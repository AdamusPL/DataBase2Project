using DBTester.Db;
using Microsoft.EntityFrameworkCore;

namespace DBTester.Benchmarks;

public class SemesterSearch : BenchmarkBase
{
    private int _numberOfSemesters = 1_000_000;
    private const int BatchSize = 2000;

    public SemesterSearch(TestingContext testingContext)
        : base(testingContext)
    { }

    protected override async Task Benchmark()
    {
        var now = DateTime.Now;

        await _testingContext.AddAsync(new Semester
        {
            Id = RandomId,
            StartDate = new DateTime(),
            EndDate = new DateTime()
        });

        await _testingContext.AddAsync(new Semester
        {
            Id = RandomId,
            StartDate = now,
            EndDate = now
        });

        await _testingContext.SaveChangesAsync();

        await RunTest("Search oldest", async () =>
        {
            var findEntity = await _testingContext.Semesters
                .FirstOrDefaultAsync(x => x.StartDate == now);
        });

        await RunTest("Search oldest", async () =>
        {
            var now = new DateTime();
            var findEntity = await _testingContext.Semesters
                .FirstOrDefaultAsync(x => x.StartDate == now);
        });
    }

    protected override async Task PrepareData()
    {
        var rnd = new Random();

        for (int i = 0; i < _numberOfSemesters; ++i)
        {
            var year = rnd.Next(2000, 2020);
            var month = rnd.Next(1, 12);

            await _testingContext.AddAsync(new Semester
            {
                Id = RandomId,
                StartDate = new DateTime(year, month, 1),
                EndDate = new DateTime(year, month, 12)
            });

            if ((i + 1) % BatchSize == 0)
            {
                Console.WriteLine($"Inserting {(i + 1) / BatchSize}/{_numberOfSemesters / BatchSize}");
                await _testingContext.SaveChangesAsync();
            }
        }
    }

    private string RandomId =>
        Guid.NewGuid().ToString().Substring(0, 20);
}
