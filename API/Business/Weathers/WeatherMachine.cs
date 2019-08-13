using System.Collections.Generic;
using System.Linq;
using API.Business.Weathers;
using API.Business.Weathers.Calculators;
using API.Business.Weathers.Contexts;
using API.Business.Weathers.PeriodsByWeather;
using API.Business.Weathers.Validators;
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

        public IEnumerable<PeriodsByWeather> Predict()
        {
            var weathersByDay = new List<WeatherByDay>();

            var betasoideContext = new PlanetCalculationContext(new Planet("betasoide", 2000, -3));
            var ferengieContext = new PlanetCalculationContext(new Planet("ferengie", 500, -1));
            var vulcanoContext = new PlanetCalculationContext(new Planet("vulcano", 1000, 5));
            var weatherByDay = PredictDay(1, betasoideContext, ferengieContext, vulcanoContext);
            weathersByDay.Add(weatherByDay);

            for (int day = 2; day <= 3600; day++)
            {
                weatherByDay = PredictDay(day, betasoideContext, ferengieContext, vulcanoContext);
                weathersByDay.Add(weatherByDay);
            }

            var periodsByWeatherForBetasoide = periodsByWeatherFactory.Create(betasoideContext);
            var periodsByWeatherForFerengie = periodsByWeatherFactory.Create(ferengieContext);
            var periodsByWeatherForvulcano = periodsByWeatherFactory.Create(vulcanoContext);
            
            return periodsByWeatherForBetasoide.Concat(periodsByWeatherForFerengie).Concat(periodsByWeatherForvulcano);
        }

        private WeatherByDay PredictDay(int day, 
                                        PlanetCalculationContext betasoideContext,
                                        PlanetCalculationContext ferengieContext, 
                                        PlanetCalculationContext vulcanoContext)
        {
            var weather = PredictBy(day, betasoideContext.Planet, ferengieContext.Planet, vulcanoContext.Planet);
            betasoideContext.UpdateContext(weather.Type);
            ferengieContext.UpdateContext(weather.Type);
            vulcanoContext.UpdateContext(weather.Type);

            return new WeatherByDay(weather.Name, day);
        }

        public Weather PredictBy(int day, Planet betasoide, Planet ferengie, Planet vulcano)
        {
            var betasoidePosition = geometricCalculator.CalculteCoordinates(betasoide.DistanceToSun, betasoide.AngularVelocity, day); // 2 dias para dar una vuelta 
            var ferengiePosition = geometricCalculator.CalculteCoordinates(ferengie.DistanceToSun, ferengie.AngularVelocity, day); // 6 dias para una veulta
            var vulcanoPosition = geometricCalculator.CalculteCoordinates(vulcano.DistanceToSun, vulcano.AngularVelocity, day); // 1 dia para una vuelta
            var weather = weatherValidator.DeterminateWheater(betasoidePosition, ferengiePosition, vulcanoPosition);

            return weather;
        }

        public Weather PredictBy(int day)
        {
            var betasoide = new Planet("betasoide", 2000, -3);
            var ferengie = new Planet("ferengie", 500, -1);
            var vulcano = new Planet("vulcano", 1000, 5);
            var weather = PredictBy(day, betasoide, ferengie, vulcano);

            return weather;
        }
    }
}
