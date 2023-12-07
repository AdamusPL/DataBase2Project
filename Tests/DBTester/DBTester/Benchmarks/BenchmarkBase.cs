using DBTester.Db;

namespace DBTester.Benchmarks;

public abstract class BenchmarkBase : IBenchmark
{
    protected readonly TestingContext _testingContext;

    protected BenchmarkBase(TestingContext testingContext)
    {
        _testingContext = testingContext;
    }

    public async Task Run()
    {
        using var transaction = _testingContext.Database.BeginTransaction();

        try
        {
            await PrepareData();
            await Benchmark();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            transaction.Rollback();
        }
    }

    protected async Task RunTest(string name, Func<Task> actionToTest)
    {
        Console.WriteLine($"{name} started");

        var start = CurrentTimestamp;
        await actionToTest();
        var elapsed = CurrentTimestamp - start;


        Console.WriteLine($"{name} ended after {elapsed / 1000_000_000} seconds");
    }

    protected long CurrentTimestamp => DateTime.Now.ToFileTime();
    protected abstract Task PrepareData();
    protected abstract Task Benchmark();
}
