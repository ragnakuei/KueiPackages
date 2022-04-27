namespace KueiPackagesTests.DapperTests.GenerateSqlServiceTests
{
    public class ClassC
    {
        [Column(ColumnType.Sn)]
        public Guid? Guid { get; set; }

        [Column(ColumnType.Fn)]
        public Guid? ParentGuid { get; set; }

        public string Name { get; set; }

        public int? Age { get; set; }

        [Column(ColumnType.DeleteFlag)]
        public int DataStatusId { get; set; }
    }
}
