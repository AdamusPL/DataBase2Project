using DBTester.Benchmarks;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddScoped<IBenchmark, SemesterSearch>();
var provider = services.BuildServiceProvider();

var benchmarks = provider.GetRequiredService<IEnumerable<IBenchmark>>();

foreach (var benchmark in benchmarks)
{
    await benchmark.Run();
    benchmark.Dispose();
}