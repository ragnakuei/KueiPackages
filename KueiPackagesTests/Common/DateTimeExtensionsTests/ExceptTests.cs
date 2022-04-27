namespace KueiPackagesTests.Common.DateTimeExtensionsTests
{
    public class ExceptTests
    {
        [Test]
        public void 沒有排除時間()
        {
            var sourcePeriod = new DurationDto(new DateTime(2020, 2, 3, 8,  0, 0),
                                               new DateTime(2020, 2, 3, 17, 0, 0));

            var exceptPeriods = new DurationDto[0];

            var actual = sourcePeriod.Except(exceptPeriods).ToArray();
            var expected = new[]
                           {
                               new DurationDto(new DateTime(2020, 2, 3, 8,  0, 0),
                                               new DateTime(2020, 2, 3, 17, 0, 0)),
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void 一個排除時間_起始時間相同()
        {
            var sourcePeriod = new DurationDto(new DateTime(2020, 2, 3, 8,  0, 0),
                                               new DateTime(2020, 2, 3, 17, 0, 0));

            var exceptPeriods = new[]
                                {
                                    new DurationDto(new DateTime(2020, 2, 3, 8, 0, 0),
                                                    new DateTime(2020, 2, 3, 9, 5, 0)),
                                };

            var actual = sourcePeriod.Except(exceptPeriods).ToArray();
            var expected = new[]
                           {
                               new DurationDto(new DateTime(2020, 2, 3, 9,  5, 0),
                                               new DateTime(2020, 2, 3, 17, 0, 0)),
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void 一個排除時間_在中間()
        {
            var sourcePeriod = new DurationDto(new DateTime(2020, 2, 3, 8,  0, 0),
                                               new DateTime(2020, 2, 3, 17, 0, 0));

            var exceptPeriods = new[]
                                {
                                    new DurationDto(new DateTime(2020, 2, 3, 8, 5, 0),
                                                    new DateTime(2020, 2, 3, 9, 5, 0)),
                                };

            var actual = sourcePeriod.Except(exceptPeriods).ToArray();
            var expected = new[]
                           {
                               new DurationDto(new DateTime(2020, 2, 3, 8, 0, 0),
                                               new DateTime(2020, 2, 3, 8, 5, 0)),
                               new DurationDto(new DateTime(2020, 2, 3, 9,  5, 0),
                                               new DateTime(2020, 2, 3, 17, 0, 0)),
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void 一個排除時間_結束時間相同()
        {
            var sourcePeriod = new DurationDto(new DateTime(2020, 2, 3, 8,  0, 0),
                                               new DateTime(2020, 2, 3, 17, 0, 0));

            var exceptPeriods = new[]
                                {
                                    new DurationDto(new DateTime(2020, 2, 3, 8,  5, 0),
                                                    new DateTime(2020, 2, 3, 17, 0, 0)),
                                };

            var actual = sourcePeriod.Except(exceptPeriods).ToArray();
            var expected = new[]
                           {
                               new DurationDto(new DateTime(2020, 2, 3, 8, 0, 0),
                                               new DateTime(2020, 2, 3, 8, 5, 0)),
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void 一個排除時間_起始結束時間相同()
        {
            var sourcePeriod = new DurationDto(new DateTime(2020, 2, 3, 8,  0, 0),
                                               new DateTime(2020, 2, 3, 17, 0, 0));

            var exceptPeriods = new[]
                                {
                                    new DurationDto(new DateTime(2020, 2, 3, 8,  0, 0),
                                                    new DateTime(2020, 2, 3, 17, 0, 0)),
                                };

            var actual   = sourcePeriod.Except(exceptPeriods).ToArray();
            var expected = new DurationDto[0];

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void 二個排除時間_起始時間相同()
        {
            var sourcePeriod = new DurationDto(new DateTime(2020, 2, 3, 8,  0, 0),
                                               new DateTime(2020, 2, 3, 17, 0, 0));

            var exceptPeriods = new[]
                                {
                                    new DurationDto(new DateTime(2020, 2, 3, 8, 0, 0),
                                                    new DateTime(2020, 2, 3, 9, 5, 0)),
                                    new DurationDto(new DateTime(2020, 2, 3, 10, 10, 0),
                                                    new DateTime(2020, 2, 3, 11, 10, 0)),
                                };

            var actual = sourcePeriod.Except(exceptPeriods).ToArray();
            var expected = new[]
                           {
                               new DurationDto(new DateTime(2020, 2, 3, 9,  5,  0),
                                               new DateTime(2020, 2, 3, 10, 10, 0)),
                               new DurationDto(new DateTime(2020, 2, 3, 11, 10, 0),
                                               new DateTime(2020, 2, 3, 17, 0,  0)),
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void 二個排除時間_在中間()
        {
            var sourcePeriod = new DurationDto(new DateTime(2020, 2, 3, 8,  0, 0),
                                               new DateTime(2020, 2, 3, 17, 0, 0));

            var exceptPeriods = new[]
                                {
                                    new DurationDto(new DateTime(2020, 2, 3, 8, 5, 0),
                                                    new DateTime(2020, 2, 3, 9, 5, 0)),
                                    new DurationDto(new DateTime(2020, 2, 3, 10, 10, 0),
                                                    new DateTime(2020, 2, 3, 11, 10, 0)),
                                };

            var actual = sourcePeriod.Except(exceptPeriods).ToArray();
            var expected = new[]
                           {
                               new DurationDto(new DateTime(2020, 2, 3, 8, 0, 0),
                                               new DateTime(2020, 2, 3, 8, 5, 0)),
                               new DurationDto(new DateTime(2020, 2, 3, 9,  5,  0),
                                               new DateTime(2020, 2, 3, 10, 10, 0)),
                               new DurationDto(new DateTime(2020, 2, 3, 11, 10, 0),
                                               new DateTime(2020, 2, 3, 17, 0,  0)),
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void 二個排除時間_結束時間相同()
        {
            var sourcePeriod = new DurationDto(new DateTime(2020, 2, 3, 8,  0, 0),
                                               new DateTime(2020, 2, 3, 17, 0, 0));

            var exceptPeriods = new[]
                                {
                                    new DurationDto(new DateTime(2020, 2, 3, 8, 5, 0),
                                                    new DateTime(2020, 2, 3, 9, 5, 0)),
                                    new DurationDto(new DateTime(2020, 2, 3, 10, 10, 0),
                                                    new DateTime(2020, 2, 3, 17, 0,  0)),
                                };

            var actual = sourcePeriod.Except(exceptPeriods).ToArray();
            var expected = new[]
                           {
                               new DurationDto(new DateTime(2020, 2, 3, 8, 0, 0),
                                               new DateTime(2020, 2, 3, 8, 5, 0)),
                               new DurationDto(new DateTime(2020, 2, 3, 9,  5,  0),
                                               new DateTime(2020, 2, 3, 10, 10, 0))
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void 三個排除時間_起始時間相同()
        {
            var sourcePeriod = new DurationDto(new DateTime(2020, 2, 3, 8,  0, 0),
                                               new DateTime(2020, 2, 3, 17, 0, 0));

            var exceptPeriods = new[]
                                {
                                    new DurationDto(new DateTime(2020, 2, 3, 8, 0, 0),
                                                    new DateTime(2020, 2, 3, 9, 5, 0)),
                                    new DurationDto(new DateTime(2020, 2, 3, 10, 10, 0),
                                                    new DateTime(2020, 2, 3, 11, 10, 0)),
                                    new DurationDto(new DateTime(2020, 2, 3, 13, 00, 0),
                                                    new DateTime(2020, 2, 3, 14, 00, 0)),
                                };

            var actual = sourcePeriod.Except(exceptPeriods).ToArray();
            var expected = new[]
                           {
                               new DurationDto(new DateTime(2020, 2, 3, 9,  5,  0),
                                               new DateTime(2020, 2, 3, 10, 10, 0)),
                               new DurationDto(new DateTime(2020, 2, 3, 11, 10, 0),
                                               new DateTime(2020, 2, 3, 13, 0,  0)),
                               new DurationDto(new DateTime(2020, 2, 3, 14, 0, 0),
                                               new DateTime(2020, 2, 3, 17, 0, 0)),
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void 三個排除時間_在中間()
        {
            var sourcePeriod = new DurationDto(new DateTime(2020, 2, 3, 8,  0, 0),
                                               new DateTime(2020, 2, 3, 17, 0, 0));

            var exceptPeriods = new[]
                                {
                                    new DurationDto(new DateTime(2020, 2, 3, 8, 5, 0),
                                                    new DateTime(2020, 2, 3, 9, 5, 0)),
                                    new DurationDto(new DateTime(2020, 2, 3, 10, 10, 0),
                                                    new DateTime(2020, 2, 3, 11, 10, 0)),
                                    new DurationDto(new DateTime(2020, 2, 3, 13, 00, 0),
                                                    new DateTime(2020, 2, 3, 14, 00, 0)),
                                };

            var actual = sourcePeriod.Except(exceptPeriods).ToArray();
            var expected = new[]
                           {
                               new DurationDto(new DateTime(2020, 2, 3, 8, 0, 0),
                                               new DateTime(2020, 2, 3, 8, 5, 0)),
                               new DurationDto(new DateTime(2020, 2, 3, 9,  5,  0),
                                               new DateTime(2020, 2, 3, 10, 10, 0)),
                               new DurationDto(new DateTime(2020, 2, 3, 11, 10, 0),
                                               new DateTime(2020, 2, 3, 13, 0,  0)),
                               new DurationDto(new DateTime(2020, 2, 3, 14, 0, 0),
                                               new DateTime(2020, 2, 3, 17, 0, 0)),
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void 三個排除時間_結束時間相同()
        {
            var sourcePeriod = new DurationDto(new DateTime(2020, 2, 3, 8,  0, 0),
                                               new DateTime(2020, 2, 3, 17, 0, 0));

            var exceptPeriods = new[]
                                {
                                    new DurationDto(new DateTime(2020, 2, 3, 8, 5, 0),
                                                    new DateTime(2020, 2, 3, 9, 5, 0)),
                                    new DurationDto(new DateTime(2020, 2, 3, 10, 10, 0),
                                                    new DateTime(2020, 2, 3, 11, 0,  0)),
                                    new DurationDto(new DateTime(2020, 2, 3, 14, 0, 0),
                                                    new DateTime(2020, 2, 3, 17, 0, 0)),
                                };

            var actual = sourcePeriod.Except(exceptPeriods).ToArray();
            var expected = new[]
                           {
                               new DurationDto(new DateTime(2020, 2, 3, 8, 0, 0),
                                               new DateTime(2020, 2, 3, 8, 5, 0)),
                               new DurationDto(new DateTime(2020, 2, 3, 9,  5,  0),
                                               new DateTime(2020, 2, 3, 10, 10, 0)),
                               new DurationDto(new DateTime(2020, 2, 3, 11, 0, 0),
                                               new DateTime(2020, 2, 3, 14, 0, 0)),
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void 一個排除時間_起始時間過早()
        {
            var sourcePeriod = new DurationDto(new DateTime(2020, 2, 3, 8,  0, 0),
                                               new DateTime(2020, 2, 3, 17, 0, 0));

            var exceptPeriods = new[]
                                {
                                    new DurationDto(new DateTime(2020, 2, 3, 7, 0, 0),
                                                    new DateTime(2020, 2, 3, 9, 5, 0)),
                                };

            var actual = sourcePeriod.Except(exceptPeriods).ToArray();
            var expected = new[]
                           {
                               new DurationDto(new DateTime(2020, 2, 3, 9,  5,  0),
                                               new DateTime(2020, 2, 3, 17, 0, 0)),
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void 一個排除時間_結束時間過晚()
        {
            var sourcePeriod = new DurationDto(new DateTime(2020, 2, 3, 8,  0, 0),
                                               new DateTime(2020, 2, 3, 17, 0, 0));

            var exceptPeriods = new[]
                                {
                                    new DurationDto(new DateTime(2020, 2, 3, 9,  0, 0),
                                                    new DateTime(2020, 2, 3, 17, 5, 0)),
                                };

            var actual = sourcePeriod.Except(exceptPeriods).ToArray();
            var expected = new[]
                           {
                               new DurationDto(new DateTime(2020, 2, 3, 8,  0, 0), new DateTime(2020, 2, 3, 9,  0, 0)),
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void 一個排除時間_起始結束過早()
        {
            var sourcePeriod = new DurationDto(new DateTime(2020, 2, 3, 9, 0, 0), new DateTime(2020, 2, 3, 10, 0, 0));

            var exceptPeriods = new[]
                                {
                                    new DurationDto(new DateTime(2020, 2, 3, 8, 0, 0), new DateTime(2020, 2, 3, 8, 30, 0)),
                                };

            var actual = sourcePeriod.Except(exceptPeriods).ToArray();
            var expected = new[]
                           {
                               new DurationDto(new DateTime(2020, 2, 3, 9, 0, 0), new DateTime(2020, 2, 3, 10, 0, 0))
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void 一個排除時間_起始結束過晚()
        {
            var sourcePeriod = new DurationDto(new DateTime(2020, 2, 3, 9, 0, 0), new DateTime(2020, 2, 3, 10, 0, 0));

            var exceptPeriods = new[]
                                {
                                    new DurationDto(new DateTime(2020, 2, 3, 11, 0, 0), new DateTime(2020, 2, 3, 11, 30, 0)),
                                };

            var actual = sourcePeriod.Except(exceptPeriods).ToArray();
            var expected = new[]
                           {
                               new DurationDto(new DateTime(2020, 2, 3, 9, 0, 0), new DateTime(2020, 2, 3, 10, 0, 0))
                           };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void 二個排除時間_排除時間有重疊()
        {
            var sourcePeriod = new DurationDto(new DateTime(2020, 2, 3, 8,  0, 0), new DateTime(2020, 2, 3, 17, 0, 0));

            var exceptPeriods = new[]
                                {
                                    new DurationDto(new DateTime(2020, 2, 3, 9,  0, 0), new DateTime(2020, 2, 3, 10, 0, 0)),
                                    new DurationDto(new DateTime(2020, 2, 3, 9,  30, 0), new DateTime(2020, 2, 3, 11, 0,  0)),
                                };

            var actual = sourcePeriod.Except(exceptPeriods).ToArray();
            var expected = new[]
                           {
                               new DurationDto(new DateTime(2020, 2, 3, 8, 0, 0), new DateTime(2020, 2, 3, 9, 0, 0)),
                               new DurationDto(new DateTime(2020, 2, 3, 11, 0, 0), new DateTime(2020, 2, 3, 17, 0, 0)),
                           };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
