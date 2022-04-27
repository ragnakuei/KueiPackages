using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace DapperVsEfCore
{
    public class Program
    {
        static void Main(string[] args)
        {
            var seedDataService = DiFactory.GetService<SeedDataService>();

            seedDataService.SeedData();

            // new RunService().Run();
            var summary = BenchmarkRunner.Run<TestRunner>();

            seedDataService.ClearSeedData();
        }
    }

    public class TestRunner
    {
        [Benchmark]
        public void EfCore_FromSqlRaw() => DiFactory.GetService<EfCoreService>().FromSqlRaw_SqlParameter();

        [Benchmark]
        public void EfCore_FromSqlRaw_Where() => DiFactory.GetService<EfCoreService>().FromSqlRaw_Where();

        [Benchmark]
        public void EfCore_QueryMultiple() => DiFactory.GetService<EfCoreService>().QueryMultiple();

        [Benchmark]
        public void Dapper() => DiFactory.GetService<DapperService>().Run();
    }
}
