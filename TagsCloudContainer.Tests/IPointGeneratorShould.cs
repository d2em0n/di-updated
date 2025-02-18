﻿using FluentAssertions;
using System.Drawing;
using TagsCloudContainer.PointGenerators;

namespace TagsCloudContainer.Tests
{
    [TestFixture]
    public class IPointGeneratorShould
    {
        [TestCaseSource(nameof(TestCases))]
        public void GeneratePoints_MovingAwayFromTheStartFor(IPointGenerator pointGenerator)
        {
            var start = new Point(0, 0);
            var points = pointGenerator.GeneratePoints(start);
            var nearPoint = points.ElementAt(100);
            var farPoint = points.ElementAt(1000);

            DistanceBetween(start, nearPoint).Should().BeLessThan(DistanceBetween(start, farPoint));
        }

        [TestCaseSource(nameof(TestCases))]
        public void GeneratePoints_ReturnsStartAsFirstPointFor(IPointGenerator pointGenerator)
        {
            var start = new Point(100, 100);
            var firstReturned = pointGenerator.GeneratePoints(start)
                .First();

            firstReturned.Should().BeEquivalentTo(start);
        }

        private static IEnumerable<IPointGenerator> TestCases()
        {
            yield return new ArchemedianSpiral();
            yield return new HeartShaped();
            yield return new DeltaShaped();
        }

        private static int DistanceBetween(Point start, Point destination)
        {
            return (int)Math.Sqrt((start.X - destination.X) * (start.X - destination.X) +
                                   (start.Y - destination.Y) * (start.Y - destination.Y));
        }
    }
}
