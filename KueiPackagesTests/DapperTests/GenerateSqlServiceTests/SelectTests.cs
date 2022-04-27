namespace KueiPackagesTests.DapperTests.GenerateSqlServiceTests
{
    public class SelectTests
    {
        [Test]
        public void Case01()
        {
            var target = new GenerateSqlService();
            var actual = target.Generate<ClassA>(SqlScriptType.Select);

            var expected = @"SELECT Id, Name, Age
FROM [dbo].[ClassA];";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Case02_NotMapped()
        {
            var target = new GenerateSqlService();
            var actual = target.Generate<ClassB>(SqlScriptType.Select);

            var expected = @"SELECT Name, Age
FROM [dbo].[ClassB];";

            Assert.AreEqual(expected, actual);
        }
    }
}
