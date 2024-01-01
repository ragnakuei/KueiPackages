using System.Reflection;
using KueiPackages.AOP;
using KueiPackagesTests.AOP.Models;

namespace KueiPackagesTests.AOP;

[Category("AOP Tests")]
public class TestClass1Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestMethod()
    {
        var (target, logger) = GetTestClass1();

        target.TestMethod();

        logger.Received().LogInformation("Before");
        logger.Received().LogInformation("After");
    }

    [Test]
    public void TestMethodBefore()
    {
        var (target, logger) = GetTestClass1();

        target.TestMethodOnlyBefore();

        logger.Received(1).LogInformation("Before");
    }

    [Test]
    public void TestMethodAfter()
    {
        var (target, logger) = GetTestClass1();

        target.TestMethodOnlyAfter();

        logger.Received(1).LogInformation("After");
    }

    [Test]
    public void TestLogParameters()
    {
        var (target, logger) = GetTestClass1();

        target.TestLogParameters("a", new[] { 1, 2 }, false);

        logger.Received(1).LogInformation("Before\n1th parameter \"a\"\n2th parameter [1,2]\n3th parameter false");
    }

    [Test]
    public void TestException()
    {
        var (target, logger) = GetTestClass1();

        var e = Assert.Throws<TargetInvocationException>(() => target.TestException());
        Assert.IsInstanceOf<NotImplementedException>(e.InnerException);

        logger.Received(1).LogInformation("Before");
        logger.Received(1).LogError("Exception: Exception has been thrown by the target of an invocation.");
    }

    private static (ITestClass1 target, ILogger<ITestClass1> logger) GetTestClass1()
    {
        var services = new ServiceCollection();

        services.AddAOPScoped<ITestClass1, TestClass1>();

        var logger = Substitute.For<ILogger<ITestClass1>>();
        services.AddScoped<ILogger<ITestClass1>>(_ => logger);

        var provider = services.BuildServiceProvider();

        var target = provider.GetRequiredService<ITestClass1>();

        return (target, logger);
    }
}
