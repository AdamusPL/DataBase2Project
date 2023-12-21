using System.Data;

namespace DBTester.Benchmarks;

public abstract class BenchmarkBase : IBenchmark
{
    protected readonly IDbConnection _dbConnection;

    protected BenchmarkBase(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
        _dbConnection.Open();
    }

    public async Task Run()
    {
        using var transaction = _dbConnection.BeginTransaction();

        try
        {
            await PrepareData(transaction);
            await Benchmark(transaction);
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

    protected async Task RunTest(string name, Func<Task> actionToTest, int testsCount)
    {
        Console.WriteLine($"{name} started");

        var result = 0;
        for (var i = 0; i < testsCount; i++)
        {
            var start = CurrentTimestamp;
            await actionToTest();
            var elapsed = CurrentTimestamp - start;
            result += (int)elapsed;
        }

        Console.WriteLine($"{name} ended after {result / testsCount} ms");
    }

    protected long CurrentTimestamp => DateTimeOffset.Now.ToUnixTimeMilliseconds();
    protected abstract Task PrepareData(IDbTransaction transaction);
    protected abstract Task Benchmark(IDbTransaction transaction);

    public void Dispose() => _dbConnection.Dispose();
}
