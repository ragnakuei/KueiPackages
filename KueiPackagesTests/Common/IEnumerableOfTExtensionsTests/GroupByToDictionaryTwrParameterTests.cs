namespace KueiPackagesTests.Common.IEnumerableOfTExtensionsTests
{
    public class GroupByToDictionaryTwrParameterTests
    {
        private class Test
        {
            public string Code { get; set; }

            public string SubCode { get; set; }

            public string Value { get; set; }
        }

        private IEnumerable<Test> GetTests()
            => new[]
               {
                   new Test { Code = "1", SubCode = "A", Value = "A1" },
                   new Test { Code = "1", SubCode = "A", Value = "A2" },
                   new Test { Code = "1", SubCode = "B", Value = "B1" },
                   new Test { Code = "1", SubCode = "B", Value = "B2" },
                   new Test { Code = "1", SubCode = "B", Value = "B3" },
                   new Test { Code = "2", SubCode = "A", Value = "A1" },
                   new Test { Code = "2", SubCode = "A", Value = "A2" },
                   new Test { Code = "2", SubCode = "B", Value = "B1" },
               };

        [Test]
        public void Case01()
        {
            var actual = GetTests().GroupByToDictionary(t => t.Code, t => t.SubCode);

            var expected = new Dictionary<string, Dictionary<string, List<Test>>>
                           {
                               {
                                   "1", new Dictionary<string, List<Test>>
                                        {
                                            {
                                                "A", new List<Test>
                                                     {
                                                         new Test { Code = "1", SubCode = "A", Value = "A1" },
                                                         new Test { Code = "1", SubCode = "A", Value = "A2" }
                                                     }
                                            },
                                            {
                                                "B", new List<Test>
                                                     {
                                                         new Test { Code = "1", SubCode = "B", Value = "B1" },
                                                         new Test { Code = "1", SubCode = "B", Value = "B2" },
                                                         new Test { Code = "1", SubCode = "B", Value = "B3" }
                                                     }
                                            }
                                        }
                               },
                               {
                                   "2", new Dictionary<string, List<Test>>
                                        {
                                            {
                                                "A", new List<Test>
                                                     {
                                                         new Test { Code = "2", SubCode = "A", Value = "A1" },
                                                         new Test { Code = "2", SubCode = "A", Value = "A2" }
                                                     }
                                            },
                                            {
                                                "B", new List<Test>
                                                     {
                                                         new Test { Code = "2", SubCode = "B", Value = "B1" }
                                                     }
                                            }
                                        }
                               }
                           };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
