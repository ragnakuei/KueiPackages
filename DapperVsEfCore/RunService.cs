using System.Diagnostics;

namespace DapperVsEfCore
{
    public class RunService
    {
        public void Run()
        {
            Measure("Dapper", () => DiFactory.GetService<DapperService>().Run());
            Measure("EfCore", () => DiFactory.GetService<EfCoreService>().FromSqlRaw_SqlParameter());
            Measure("EfCore", () => DiFactory.GetService<EfCoreService>().QueryMultiple());
        }

        private void Measure(string actionName, Action action)
        {
            var sw = new Stopwatch();
            sw.Start();

            action.Invoke();

            sw.Stop();
            var cost = sw.ElapsedMilliseconds;
            Console.WriteLine($"{actionName} ElapsedMilliseconds:{cost}");
        }
    }
}
