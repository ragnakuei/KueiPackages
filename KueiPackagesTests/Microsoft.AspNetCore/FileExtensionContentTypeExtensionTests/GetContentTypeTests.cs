using KueiPackages.Microsoft.AspNetCore;

namespace KueiPackagesTests.Microsoft.AspNetCore.FileExtensionContentTypeExtensionTests;

public class GetContentTypeTests
{
    [Test]
    public void xlsx()
    {
        var actual = "a.xlsx".GetContentType();

        var expected = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        Assert.AreEqual(expected, actual);
    }
}
