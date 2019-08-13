using System.Collections.Generic;
using System.Linq;
using API.Business.Weathers;
using API.Business.Weathers.Calculators;
using API.Business.Weathers.Contexts;
using API.Business.Weathers.PeriodsByWeather;
using API.Business.Weathers.PeriodsByWeathers;
using API.Business.Weathers.Results;
using API.Business.Weathers.Validators;
using API.Commons;
using API.Planets;

namespace API.Weathers
{
    public class WeatherMachine
    {
        private readonly GeometricCalculator geometricCalculator;

        private readonly WeatherValidator weatherValidator;

        private readonly PeriodsByWeatherFactory periodsByWeatherFactory;

        public WeatherMachine(GeometricCalculator geometricCalculator,
                              WeatherValidator weatherValidator,
                              PeriodsByWeatherFactory periodsByWeatherFactory)
        {
            this.geometricCalculator = geometricCalculator;
            this.weatherValidator = weatherValidator;
            this.periodsByWeatherFactory = periodsByWeatherFactory;
        }

        public FinalResults Predict()
        {
            var origin = new Point() { X = 0, Y = 0 };

            var betasoide = new Planet("betasoide", 2000, -3);
            var ferengie = new Planet("ferengie", 500, -1);
            var vulcano = new Planet("vulcano", 1000, 5);

            var betasoideContext = new PlanetCalculationContext(betasoide);
            var ferengieContext = new PlanetCalculationContext(ferengie);
            var vulcanoContext = new PlanetCalculationContext(vulcano);

            var weather = Predict(1, betasoide, ferengie, vulcano);

            betasoideContext.UpdateContext(weather.Type);
            ferengieContext.UpdateContext(weather.Type);
            vulcanoContext.UpdateContext(weather.Type);

            double maxPerimeter = 0;
            int maxRainyDay = 1;

            for (int day = 2; day <= 3600; day++)
            {
                weather = Predict(day);

                betasoideContext.UpdateContext(weather.Type);
                ferengieContext.UpdateContext(weather.Type);
                vulcanoContext.UpdateContext(weather.Type);

                if (weather.Type == WeatherType.Rainy)
                {
                    var betasoidePosition = geometricCalculator.CalculteCoordinates(betasoide.DistanceToSun, betasoide.AngularVelocity, day);
                    var ferengiePosition = geometricCalculator.CalculteCoordinates(ferengie.DistanceToSun, ferengie.AngularVelocity, day);
                    var vulcanoPosition = geometricCalculator.CalculteCoordinates(vulcano.DistanceToSun, vulcano.AngularVelocity, day);
                    var perimeterTriangule = geometricCalculator.CalculatePerimeterOfTriangule(betasoidePosition, ferengiePosition, vulcanoPosition);
                    if (perimeterTriangule > maxPerimeter)
                    {
                        maxPerimeter = perimeterTriangule;
                        maxRainyDay = day;
                    }
                }
            }

            // pass to finarl result factory
            var periodsByWeatherForBetasoide = periodsByWeatherFactory.Create(betasoideContext);
            var periodsByWeatherForFerengie = periodsByWeatherFactory.Create(ferengieContext);
            var periodsByWeatherForvulcano = periodsByWeatherFactory.Create(vulcanoContext);
            
            var periosByWeather = periodsByWeatherForBetasoide.Concat(periodsByWeatherForFerengie).Concat(periodsByWeatherForvulcano);

            return new FinalResults() { PeriodsByWeather = periosByWeather, RainyMaxValueDate = maxRainyDay };
        }

        public Weather Predict(int day, Planet betasoide, Planet ferengie, Planet vulcano)
        {
            var origin = new Point() { X = 0, Y = 0 };
            var betasoidePosition = geometricCalculator.CalculteCoordinates(betasoide.DistanceToSun, betasoide.AngularVelocity, day); 
            var ferengiePosition = geometricCalculator.CalculteCoordinates(ferengie.DistanceToSun, ferengie.AngularVelocity, day);
            var vulcanoPosition = geometricCalculator.CalculteCoordinates(vulcano.DistanceToSun, vulcano.AngularVelocity, day);

            var totalAreaOfPlanets = geometricCalculator.PoligonArea(betasoidePosition, ferengiePosition, vulcanoPosition);
            var totalAreaOfPlanetsWithOrigin = geometricCalculator.PoligonArea(betasoidePosition, ferengiePosition, vulcanoPosition, origin);

            var weather = weatherValidator.DeterminateWheater(totalAreaOfPlanets, totalAreaOfPlanetsWithOrigin);

            return weather;
        }

        public Weather Predict(int day)
        {
            var betasoide = new Planet("betasoide", 2000, -3);
            var ferengie = new Planet("ferengie", 500, -1);
            var vulcano = new Planet("vulcano", 1000, 5);

            return Predict(day, betasoide, ferengie, vulcano);
        }
    }
}
