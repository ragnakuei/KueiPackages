namespace KueiPackagesTests.Common.TypeExtensionsTests;

public class IsDictionaryTests
{
    [Test]
    public void 傳入_Dictionary_型別()
    {
        var d = new Dictionary<string, string>();
        
        var actual = d.GetType().IsDictionary();

        Assert.True(actual);
    }
    
    [Test]
    public void 傳入非_Dictionary_型別()
    {
        var d = new object();
        
        var actual = d.GetType().IsDictionary();

        Assert.False(actual);
    }
}
