using System.Text.Json.Serialization;
using KueiPackages.Types;

namespace KueiPackagesTests.System.Text.Json;

[Category("ArrayOfTJsonConverter")]
public class ArrayOfTJsonConverterTests
{
    private JsonSerializerOptions _jsonSerializerOptions
        = new JsonSerializerOptions
          {
              Converters = { new ArrayOfTJsonConverter() },
          };

    private class TestDto1
    {
        public Array<int> Ids { get; set; }
    }

    private class TestDto2
    {
        public int Id { get; set; }
    }

    [Test]
    public void ToJsonArrayOfInt()
    {
        var actual = new TestDto1
                     {
                         Ids = new Array<int>(1, 2, 3),
                     }.ToJson(_jsonSerializerOptions);

        var expected = "{\"Ids\":[1,2,3]}";

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void ToJsonArrayOfObject1()
    {
        var actual = new Array<TestDto2>
                     {
                         new TestDto2 { Id = 1, },
                         new TestDto2 { Id = 2, },
                         new TestDto2 { Id = 3, },
                     }.ToJson(_jsonSerializerOptions);

        var expected = "[{\"Id\":1},{\"Id\":2},{\"Id\":3}]";

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void ToJsonArrayOfObject2()
    {
        var actual = new Array<TestDto1>
                     {
                         new TestDto1 { Ids = new Array<int>(items: 1), },
                         new TestDto1 { Ids = new Array<int>(items: 2), },
                         new TestDto1 { Ids = new Array<int>(items: 3), },
                     }.ToJson(_jsonSerializerOptions);

        var expected = "[{\"Ids\":[1]},{\"Ids\":[2]},{\"Ids\":[3]}]";

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void FromJsonArrayOfInt()
    {
        var actual = "{\"Ids\":[1,2,3]}".ParseJson<TestDto1>(_jsonSerializerOptions);

        var expected = new TestDto1
                       {
                           Ids = new Array<int>(1, 2, 3),
                       };

        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void FromJsonArrayOfObject1()
    {
        var actual = "[{\"Id\":1},{\"Id\":2},{\"Id\":3}]".ParseJson<Array<TestDto2>>(_jsonSerializerOptions);

        var expected = new Array<TestDto2>
                       {
                           new TestDto2 { Id = 1, },
                           new TestDto2 { Id = 2, },
                           new TestDto2 { Id = 3, },
                       };

        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void FromJsonArrayOfObject2()
    {
        var actual = "[{\"Ids\":[1]},{\"Ids\":[2]},{\"Ids\":[3]}]".ParseJson<Array<TestDto1>>(_jsonSerializerOptions);

        var expected = new Array<TestDto1>
                       {
                           new TestDto1 { Ids = new Array<int>(items: 1), },
                           new TestDto1 { Ids = new Array<int>(items: 2), },
                           new TestDto1 { Ids = new Array<int>(items: 3), },
                       };

        actual.Should().BeEquivalentTo(expected);
    }
}
