using System.Collections.Generic;
using WeatherApi.Business.Weather;
using WeatherApi.Business.Weathers.Calculators;
using WeatherApi.Business.Weathers.Contexts;
using WeatherApi.Business.Weathers.Validators;
using WeatherApi.Planets;

namespace WeatherApi.Weathers
{
    public class WeatherMachine
    {
        private readonly GeometricCalculator geometricCalculator;

        private readonly WeatherValidator weatherValidator;

        public WeatherMachine(GeometricCalculator geometricCalculator,
                              WeatherValidator weatherValidator)
        {
            this.geometricCalculator = geometricCalculator;
            this.weatherValidator = weatherValidator;
        }

        public IEnumerable<WeatherByDay> Predict()
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

            return weathersByDay;
        }

        private WeatherByDay PredictDay(int day,
                                        PlanetCalculationContext p1,
                                        PlanetCalculationContext p2,
                                        PlanetCalculationContext p3)
        {
            var weather = PredictWeatherBy(day);
            UpdatePlanetsDataContext(p1, weather.Type);
            UpdatePlanetsDataContext(p2, weather.Type);
            UpdatePlanetsDataContext(p3, weather.Type);

            return new WeatherByDay(weather.Name, day);
        }

        private void UpdatePlanetsDataContext(PlanetCalculationContext context, WeatherType weatherType)
        {
            context.DaysPerPeriodTracking++;
            
            if (context.DaysPerPeriodTracking <= context.Planet.Period)
            {
               if(weatherType == context.LastWeather)
                {
                    context.DaysPerPeriodWithSameWeatherTracking++;
                }
                else
                {
                    context.DaysPerPeriodWithSameWeatherTracking = 0;
                }
            }
            else
            {
                context.DaysPerPeriodTracking = 0;
                if(context.DaysPerPeriodWithSameWeatherTracking == context.Planet.Period)
                {
                    context.TotalPeriods++;
                    context.SetOcurrence(weatherType);
                }
            }
        }

        public Weather PredictWeatherBy(int day)
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
