using System.ComponentModel.DataAnnotations.Schema;

namespace KueiPackagesTests.DapperTests.GenerateSqlServiceTests
{
    public class ClassB
    {
        [NotMapped]
        [KueiPackages.Dapper.Generator.Column(ColumnType.Sn)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Age { get; set; }
    }
}
