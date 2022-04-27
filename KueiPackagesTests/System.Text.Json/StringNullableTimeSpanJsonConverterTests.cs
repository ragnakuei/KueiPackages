using System.Text.Json.Serialization;

namespace KueiPackagesTests.System.Text.Json
{
    public class StringNullableTimeSpanJsonConverterTests
    {
        private class Test
        {
            public int Id { get; set; }

            [JsonConverter(typeof(StringNullableTimeSpanJsonConverter))]
            public TimeSpan? Time { get; set; }
        }

        [Test]
        public void ToJson_Case01()
        {
            var actual = new Test
                         {
                             Id   = 1,
                             Time = new TimeSpan(12, 23, 45),
                         }.ToJson();

            var expected = "{\"Id\":1,\"Time\":\"12:23:45\"}";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ParseJson_Case01()
        {
            var actual = "{\"Id\":1,\"Time\":\"12:23:45\"}".ParseJson<Test>();
            var expected = new Test
                           {
                               Id   = 1,
                               Time = new TimeSpan(12, 23, 45),
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void ParseJson_Case02()
        {
            var actual = "{\"Id\":1,\"Time\":\"12:23\"}".ParseJson<Test>();
            var expected = new Test
                           {
                               Id   = 1,
                               Time = new TimeSpan(12, 23, 00),
                           };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
