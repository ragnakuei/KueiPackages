namespace KueiPackagesTests.DapperTests.GenerateSqlServiceTests
{
    public class UpdateTests
    {
        [Test]
        public void Case01()
        {
            var target = new GenerateSqlService();
            var actual = target.Generate<ClassA>(SqlScriptType.Update);

            var expected = @"UPDATE [dbo].[ClassA]
SET Name = @Name, Age = @Age
WHERE Id = @Id;";

            Assert.AreEqual(expected, actual);
        }
    }
}
