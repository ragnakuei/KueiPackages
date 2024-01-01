namespace KueiPackagesTests.AOP.Models;

public class TestClass1 : ITestClass1
{
    public void TestMethod()
    {
        Console.WriteLine("Hello World!");
    }

    public void TestMethodOnlyBefore()
    {
        Console.WriteLine("Hello World!");
    }

    public void TestMethodOnlyAfter()
    {
        Console.WriteLine("Hello World!");
    }

    public void TestException()
    {
        throw new NotImplementedException();
    }

    public void TestLogParameters(string s, int[] ints, bool isTrue)
    {
        Console.WriteLine("Hello World!");
    }
}