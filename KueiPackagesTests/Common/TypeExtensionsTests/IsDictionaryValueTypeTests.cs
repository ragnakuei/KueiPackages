namespace KueiPackagesTests.Common.TypeExtensionsTests;

public class IsDictionaryValueTypeTests
{
    [Test]
    public void Dictionary_TValue_型別_為字串型別()
    {
        var d = new Dictionary<string, string>();
        
        var actual = d.GetType().IsDictionaryValueType(typeof(string));

        Assert.True(actual);
    }
    
    [Test]
    public void 傳入_非Dictionary_型別()
    {
        var d = new object();
        
        var actual = d.GetType().IsDictionaryValueType(typeof(string));

        Assert.False(actual);
    }
}
