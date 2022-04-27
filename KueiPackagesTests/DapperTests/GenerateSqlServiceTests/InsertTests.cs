namespace KueiPackagesTests.DapperTests.GenerateSqlServiceTests
{
    public class InsertTests
    {
        [Test]
        public void Case01()
        {
            var target = new GenerateSqlService();
            var actual = target.Generate<ClassA>(SqlScriptType.Insert);

            var expected = @"INSERT INTO [dbo].[ClassA] (Id, Name, Age)
VALUES (@Id, @Name, @Age);";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Case02_NotMapped()
        {
            var target = new GenerateSqlService();
            var actual = target.Generate<ClassB>(SqlScriptType.Insert);

            var expected = @"INSERT INTO [dbo].[ClassB] (Name, Age)
VALUES (@Name, @Age);";

            Assert.AreEqual(expected, actual);
        }
    }
}
