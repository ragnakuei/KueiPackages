namespace KueiPackagesTests.DapperTests.GenerateSqlServiceTests
{
    public class ClassA
    {
        [KueiPackages.Dapper.Generator.Column(ColumnType.Sn)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Age { get; set; }
    }
}
