namespace DBTester.Benchmarks;

public interface IBenchmark : IDisposable
{
    Task Run();
}
