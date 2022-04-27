namespace KueiPackagesTests.DapperTests.GenerateSqlServiceTests
{
    public class DeleteTests
    {
        [Test]
        public void Case01()
        {
            var target = new GenerateSqlService();
            var actual = target.Generate<ClassA>(SqlScriptType.Delete);

            var expected = @"DELETE [dbo].[ClassA]
WHERE Id = @Id;";

            Assert.AreEqual(expected, actual);
        }
    }
}
