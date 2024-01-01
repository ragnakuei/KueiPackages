using KueiPackages.AOP.Models;

namespace KueiPackagesTests.AOP.Models;

public interface ITestClass1
{
    [AOP(before: "Before",
         after: "After",
         exception: "Exception")]
    void TestMethod();

    [AOP(before: "Before")]
    void TestMethodOnlyBefore();

    [AOP(after: "After")]
    void TestMethodOnlyAfter();

    [AOP(before: "Before",
         after: "After",
         exception: "Exception")]
    void TestException();

    [AOP(before: "Before", isLogParameter: true)]
    void TestLogParameters(string s, int[] ints, bool isTrue);
}