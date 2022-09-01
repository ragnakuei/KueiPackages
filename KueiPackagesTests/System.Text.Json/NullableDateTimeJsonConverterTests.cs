using System.Text.Json.Serialization;

namespace KueiPackagesTests.System.Text.Json
{
    public class NullableDateTimeJsonConverterTests
    {
        private class NullableDateTimeTestJsonConverter : NullableDateTimeJsonConverter
        {
            protected override string DateTimeFormat { get; set; } = "yyyy-MM-dd";
        }

        private class Test
        {
            public int Id { get; set; }

            [JsonConverter(typeof(NullableDateTimeTestJsonConverter))]
            public DateTime? Date { get; set; }
        }

        [Test]
        public void ToJson_Case01()
        {
            var actual = new Test
                         {
                             Id   = 1,
                             Date = new DateTime(2022, 12, 23),
                         }.ToJson();

            var expected = "{\"Id\":1,\"Date\":\"2022-12-23\"}";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ParseJson_Case01()
        {
            var actual = "{\"Id\":1,\"Date\":\"2022-12-23\"}".ParseJson<Test>();
            var expected = new Test
                           {
                               Id   = 1,
                               Date = new DateTime(2022, 12, 23),
                           };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
