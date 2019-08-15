using System;
using API.Business.Weathers;
using API.Business.Weathers.Calculators;
using API.Business.Weathers.Validators;
using API.Commons;
using Xunit;

namespace UnitTests
{
    public class WeatherValidatorTest
    {
        private readonly GeometricCalculator geometricCalculator;

        private readonly WeatherValidator weatherValidator;

        public WeatherValidatorTest()
        {
            this.geometricCalculator = new GeometricCalculator();
            this.weatherValidator = new WeatherValidator(geometricCalculator);
        }

        [Fact]
        public void WhenPlanetesAreAtTheSameLineWithOriginIsDrought()
        {
            var p1 = new Point() { X = 1, Y = 1 };
            var p2 = new Point() { X = 2, Y = 2 };
            var p3 = new Point() { X = -1, Y = -1 };

            var result = this.weatherValidator.DeterminateWheater(p1, p2, p3);

            Assert.True(result.Type == WeatherType.Drought);
        }

        [Fact]
        public void WhenPlanetesAreAtTheSameLineWithoutOriginIsExcellentWeather()
        {
            var p1 = new Point() { X = 2, Y = 1 };
            var p2 = new Point() { X = 2, Y = 5 };
            var p3 = new Point() { X = 2, Y = -1 };

            var result = this.weatherValidator.DeterminateWheater(p1, p2, p3);

            Assert.True(result.Type == WeatherType.IdealConditions);
        }

    }
}
