using System;
using System.Collections.Generic;
using WeatherPredictionMachine.Business.Weather;
using WeatherPredictionMachine.Commons;
using WeatherPredictionMachine.Weathers.Updaters;

namespace WeatherPredictionMachine.Weathers
{
    public class WeatherMachine
    {
        private readonly WeatherCountUpdater weatherCountUpdater;

        public WeatherMachine()
        {
            this.weatherCountUpdater = new WeatherCountUpdater();
        }

        public IEnumerable<WeatherByDay> Predict()
        {
            var weatherResults = new WeatherResults();
            var weathersByDay = new List<WeatherByDay>();
            for (int day = 1; day <= 360; day++)
            {
                var weather = PredictWeatherBy(day);
                weatherCountUpdater.Update(weatherResults, weather.Type);
                var weatherByDay = new WeatherByDay(weather.Name, day);

                weathersByDay.Add(weatherByDay);
            }

            return weathersByDay;
        }

        public Weather PredictWeatherBy(int day)
        {
            // I can retreive the planets from database
            var betasoidePosition = CalculteCoordinates(2000, -3, day); // 2 dias para dar una vuelta 
            var ferengiePosition = CalculteCoordinates(500, -1, day); // 6 dias para una veulta
            var vulcanoPosition = CalculteCoordinates(1000, 5, day); // 1 dia para una vuelta
            var weather = DeterminateWheater(betasoidePosition, ferengiePosition, vulcanoPosition);

            return weather;
        }

        private bool ThereIsDrought(Point p1, Point p2)
        {
            var m = (p2.Y - p1.Y) / (p2.X - p1.X);
            var independentConstant = p1.Y - m * p1.X;
            return independentConstant == 0;
        }

        private bool ArePlanetsAligned(Point referencePoint, Point p2, Point p3)
        {
            var m1 = (p2.Y - referencePoint.Y) / (p2.X - referencePoint.X);
            var m2 = (p3.Y - referencePoint.Y) / (p3.X - referencePoint.X);

            return m1 == m2;
        }

        private bool IsRainyDay(Point p1, Point p2, Point p3)
        {
            var originPoint = new Point { X = 0, Y = 0 };
            return PoligonArea(new Point[] { p1, p2, p3 }) > PoligonArea(new Point[] { p1, p2, p3, originPoint });
        }

        private Weather DeterminateWheater(Point p1, Point p2, Point p3)
        {
            if (ArePlanetsAligned(p1, p2, p3))
            {
                if (ThereIsDrought(p1, p2 ))
                {
                    // Sequia
                    return new Weather("Sequia", WeatherType.Drought);
                }
                // Condiciones optimas de presion y temperatura
                return new Weather("condiciones ideales", WeatherType.IdealConditions);
            }
            
            if (IsRainyDay(p1, p2, p3))
            {
                return new Weather("LLuvia", WeatherType.Rainy);
            }

            return new Weather("Incorrecto", WeatherType.NotDefined);
        }

        private float PoligonArea(params Point[] points)
        {
            // Add the first point to the end.
            var lengthPoints = points.Length;
            Point[] pts = new Point[lengthPoints + 1];
            points.CopyTo(pts, 0);
            pts[lengthPoints] = points[0];

            // Get the areas.
            float area = 0;
            for (int i = 0; i < lengthPoints; i++)
            {
                area +=
                    (pts[i + 1].X - pts[i].X) *
                    (pts[i + 1].Y + pts[i].Y) / 2;
            }

            // Return the result.
            return Math.Abs(area);
        }

        private Point CalculteCoordinates(int distance, double angularVelocity, double t)
        {
            var point = new Point();
            point.X = Convert.ToInt32(0 + distance * Math.Cos(angularVelocity * t));
            point.Y = Convert.ToInt32(0 + distance * Math.Sin(angularVelocity * t));

            return point;
        }
    }
}
