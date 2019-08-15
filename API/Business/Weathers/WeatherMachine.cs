using System.Collections.Generic;
using API.Business.Weathers;
using API.Business.Weathers.Calculators;
using API.Business.Weathers.Results;
using API.Business.Weathers.Validators;
using API.Planets;

namespace API.Weathers
{
    public class WeatherMachine
    {
        private readonly GeometricCalculator geometricCalculator;

        private readonly WeatherValidator weatherValidator;

        private Dictionary<WeatherType, int> OccurrencesByWeather = new Dictionary<WeatherType, int>();


        public WeatherMachine(GeometricCalculator geometricCalculator,
                              WeatherValidator weatherValidator)
        {
            this.geometricCalculator = geometricCalculator;
            this.weatherValidator = weatherValidator;
            OccurrencesByWeather.Add(WeatherType.Drought, 0);
            OccurrencesByWeather.Add(WeatherType.Rainy, 0);
            OccurrencesByWeather.Add(WeatherType.IdealConditions, 0);
            OccurrencesByWeather.Add(WeatherType.NotDefined, 0);
        }

        public Prediction Predict()
        {
            var betasoide = new Planet("Betasoide", 2000, -3);
            var ferengie = new Planet("Ferengie", 500, -1);
            var vulcano = new Planet("Vulcano", 1000, 5);

            double maxPerimeter = 0;
            int maxRainyDay = 0;

            for (int day = 1; day <= 3600; day++)
            {
                var weather = Predict(day);
                SetOcurrence(weather.Type);
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

            return CreatePredictionResult(maxRainyDay);
        }

        public Weather Predict(int day)
        {
            var betasoide = new Planet("betasoide", 2000, -3);
            var ferengie = new Planet("ferengie", 500, -1);
            var vulcano = new Planet("vulcano", 1000, 5);

            return Predict(day, betasoide, ferengie, vulcano);
        }

        private Weather Predict(int day, Planet betasoide, Planet ferengie, Planet vulcano)
        {
            var betasoidePosition = geometricCalculator.CalculteCoordinates(betasoide.DistanceToSun, betasoide.AngularVelocity, day);
            var ferengiePosition = geometricCalculator.CalculteCoordinates(ferengie.DistanceToSun, ferengie.AngularVelocity, day);
            var vulcanoPosition = geometricCalculator.CalculteCoordinates(vulcano.DistanceToSun, vulcano.AngularVelocity, day);
            var weather = weatherValidator.DeterminateWheater(betasoidePosition, ferengiePosition, vulcanoPosition);

            return weather;
        }

        private void SetOcurrence(WeatherType weatherType)
        {
            OccurrencesByWeather[weatherType]++;
        }

        private Prediction CreatePredictionResult(int maxRainyDay)
        {
            var predcition = new Prediction(this.OccurrencesByWeather[WeatherType.Drought],
                                            this.OccurrencesByWeather[WeatherType.Rainy],
                                            this.OccurrencesByWeather[WeatherType.IdealConditions],
                                            maxRainyDay);
            return predcition;
        }
    }
}
