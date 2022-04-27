using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KueiPackages.Extensions;
using NUnit.Framework;

namespace KueiExtensionsNetFramework45Tests.Common.IEnumerableOfTExtensions
{
    public class ToHashSetTests
    {
        [Test]
        public void Case01()
        {
            var actual = Enumerable.Range(1, 3).ToHashSet45();

            var expected = new List<int>
                           {
                               1, 2, 3
                           };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
