namespace KueiPackagesTests.System.Text.Json
{
    public class ToJsonTests
    {
        private class Test
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        [Test]
        public void Case01()
        {
            var actual = new Test
                         {
                             Id   = 1,
                             Name = "A"
                         }.ToJson();

            var expected = "{\"Id\":1,\"Name\":\"A\"}";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Case02()
        {
            var actual = new Test
                         {
                             Id   = 1,
                             Name = "A"
                         }.ToJson(new JsonSerializerOptions
                                  {
                                      WriteIndented = true
                                  });

            var expected = @"{
  ""Id"": 1,
  ""Name"": ""A""
}";

            Assert.AreEqual(expected, actual);
        }
    }
}
