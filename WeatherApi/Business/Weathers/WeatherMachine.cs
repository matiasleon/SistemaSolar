using System.Collections.Generic;
using System.Linq;
using WeatherApi.Business.Weathers;
using WeatherApi.Business.Weathers.Calculators;
using WeatherApi.Business.Weathers.Contexts;
using WeatherApi.Business.Weathers.PeriodsByWeather;
using WeatherApi.Business.Weathers.Validators;
using WeatherApi.Planets;

namespace WeatherApi.Weathers
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

            var betasoideContext = new PlanetCalculationContext(new Planet());
            var ferengieContext = new PlanetCalculationContext(new Planet());
            var vulcanoContext = new PlanetCalculationContext(new Planet());
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
                                        PlanetCalculationContext planetOneContext,
                                        PlanetCalculationContext planetTwoContext, 
                                        PlanetCalculationContext planetThreeContext)
        {
            var weather = PredictBy(day);
            planetOneContext.UpdateContext(weather.Type);
            planetTwoContext.UpdateContext(weather.Type);
            planetThreeContext.UpdateContext(weather.Type);

            return new WeatherByDay(weather.Name, day);
        }

        public Weather PredictBy(int day)
        {
            // I can retreive the planets from database
            var betasoidePosition = geometricCalculator.CalculteCoordinates(2000, -3, day); // 2 dias para dar una vuelta 
            var ferengiePosition = geometricCalculator.CalculteCoordinates(500, -1, day); // 6 dias para una veulta
            var vulcanoPosition = geometricCalculator.CalculteCoordinates(1000, 5, day); // 1 dia para una vuelta
            var weather = weatherValidator.DeterminateWheater(betasoidePosition, ferengiePosition, vulcanoPosition);

            return weather;
        }
    }
}
