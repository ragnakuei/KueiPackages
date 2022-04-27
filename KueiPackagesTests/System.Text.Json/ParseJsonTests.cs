namespace KueiPackagesTests.System.Text.Json;

public class ParseJsonTests
{
    private class Test
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    [Test]
    public void Case01()
    {
        var actual = "{\"Id\":1,\"Name\":\"A\"}".ParseJson<Test>();

        var expected = new Test
                       {
                           Id   = 1,
                           Name = "A"
                       };

        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void Case02()
    {
        var actual = @"{
  ""Id"": 1,
  ""Name"": ""A""
}".ParseJson<Test>(new JsonSerializerOptions
                   {
                       WriteIndented = true
                   });

        var expected = new Test
                       {
                           Id   = 1,
                           Name = "A"
                       };

        actual.Should().BeEquivalentTo(expected);
    }
}
