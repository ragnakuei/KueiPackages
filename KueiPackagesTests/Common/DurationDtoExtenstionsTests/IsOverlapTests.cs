namespace KueiPackagesTests.Common.DateTimeExtensionsTests
{
    public class IsOverlapTests
    {
        [Test]
        public void 三至五與二至四有重疊()
        {
            var a = new DurationDto(new DateTime(2020, 2, 3), new DateTime(2020, 2, 5));
            var b = new DurationDto(new DateTime(2020, 2, 2), new DateTime(2020, 2, 4));

            var actual = a.IsOverlap(b);

            Assert.True(actual);
        }


        [Test]
        public void 三至五與四至六有重疊()
        {
            var a = new DurationDto(new DateTime(2020, 2, 3), new DateTime(2020, 2, 5));
            var b = new DurationDto(new DateTime(2020, 2, 4), new DateTime(2020, 2, 6));

            var actual = a.IsOverlap(b);

            Assert.True(actual);
        }

        [Test]
        public void 三至五與三至五有重疊()
        {
            var a = new DurationDto(new DateTime(2020, 2, 3), new DateTime(2020, 2, 5));
            var b = new DurationDto(new DateTime(2020, 2, 3), new DateTime(2020, 2, 5));

            var actual = a.IsOverlap(b);

            Assert.True(actual);
        }

        [Test]
        public void 三至五與四至四有重疊()
        {
            var a = new DurationDto(new DateTime(2020, 2, 3), new DateTime(2020, 2, 5));
            var b = new DurationDto(new DateTime(2020, 2, 4), new DateTime(2020, 2, 4));

            var actual = a.IsOverlap(b);

            Assert.True(actual);
        }

        [Test]
        public void 三至五與二至六有重疊()
        {
            var a = new DurationDto(new DateTime(2020, 2, 3), new DateTime(2020, 2, 5));
            var b = new DurationDto(new DateTime(2020, 2, 2), new DateTime(2020, 2, 6));

            var actual = a.IsOverlap(b);

            Assert.True(actual);
        }

        [Test]
        public void 三至五與五至六沒有重疊()
        {
            var a = new DurationDto(new DateTime(2020, 2, 3), new DateTime(2020, 2, 5));
            var b = new DurationDto(new DateTime(2020, 2, 5), new DateTime(2020, 2, 6));

            var actual = a.IsOverlap(b);

            Assert.False(actual);
        }

        [Test]
        public void 三至五與六至七沒有重疊()
        {
            var a = new DurationDto(new DateTime(2020, 2, 3), new DateTime(2020, 2, 5));
            var b = new DurationDto(new DateTime(2020, 2, 6), new DateTime(2020, 2, 7));

            var actual = a.IsOverlap(b);

            Assert.False(actual);
        }

        [Test]
        public void 三至五與二至三沒有重疊()
        {
            var a = new DurationDto(new DateTime(2020, 2, 3), new DateTime(2020, 2, 5));
            var b = new DurationDto(new DateTime(2020, 2, 2), new DateTime(2020, 2, 3));

            var actual = a.IsOverlap(b);

            Assert.False(actual);
        }

        [Test]
        public void 三至五與一至二沒有重疊()
        {
            var a = new DurationDto(new DateTime(2020, 2, 3), new DateTime(2020, 2, 5));
            var b = new DurationDto(new DateTime(2020, 2, 1), new DateTime(2020, 2, 2));

            var actual = a.IsOverlap(b);

            Assert.False(actual);
        }
    }
}
