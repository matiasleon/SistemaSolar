using System;
using WeatherApi.Business.Weathers.Calculators;
using WeatherApi.Commons;
using Xunit;

namespace UnitTests
{
    public class GeometricCalculatorTest
    {
        private readonly GeometricCalculator geometricCalculator;

        public GeometricCalculatorTest()
        {
            geometricCalculator = new GeometricCalculator();
        }

        [Fact]
        public void WhenAreaPoligonIsCorrectWithThreePoints()
        {
            var point = new Point() { X = 1, Y = 2};
            var point2 = new Point() { X = 2, Y = 4 };
            var point3 = new Point() { X = -6, Y = 8 };

            var area = geometricCalculator.PoligonArea(point, point2, point3);
            var condition = area == 10;
            Assert.True(condition);
        }

        [Fact]
        public void WhenAreaPoligonHasNegativesPointsMustBeTrue()
        {
            var point = new Point() { X = 2, Y = -2 };
            var point2 = new Point() { X = -4, Y = 4 };
            var point3 = new Point() { X = -3, Y = -1 };

            var area = geometricCalculator.PoligonArea(point, point2, point3);
            var condition = area == 12;
            Assert.True(condition);
        }

        [Fact]
        public void WhenAreaPoligonHasFourPointsIncludedOriginPointsMustBeTrue()
        {
            var point = new Point() { X = 2, Y = -2 };
            var point2 = new Point() { X = -4, Y = 4 };
            var point3 = new Point() { X = -3, Y = 2 };
            var origin = new Point() { X = 0, Y = 0 };

            var area = geometricCalculator.PoligonArea(point, point2, point3, origin);
            var condition = area == 2;
            Assert.True(condition);
        }
    }
}
