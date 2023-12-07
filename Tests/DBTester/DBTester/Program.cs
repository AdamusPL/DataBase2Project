using DBTester.Benchmarks;
using DBTester.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddDbContext<TestingContext>(x => x.UseSqlServer("Server=172.28.0.1;Database=University-Main;User Id=sa;Password=zaq1@WSX;TrustServerCertificate=True"));
services.AddScoped<IBenchmark, SemesterSearch>();
var provider = services.BuildServiceProvider();

var benchmarks = provider.GetRequiredService<IEnumerable<IBenchmark>>();

foreach (var benchmark in benchmarks)
{
    await benchmark.Run();
}