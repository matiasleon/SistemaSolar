using System;
using System.Collections.Generic;
using System.Text;
using WeatherPredictionMachine.Entitites;

namespace WeatherPredictionMachine
{
    public class Machine
    {
        public IEnumerable<WeatherByDay> PredictFirstYear()
        {
            var weathersByDay = new List<WeatherByDay>();
            for (int day = 1; day <= 3600; day++)
            {
                var weather = PredictWeather(day);
                var weatherByDay = new WeatherByDay(weather, day);
                weathersByDay.Add(weatherByDay);
            }

            return weathersByDay;
        }

        public string PredictWeather(double t)
        {
            var betasoidePosition = CalculteCoordinates(2000, -3, t);
            var ferengiePosition = CalculteCoordinates(500, -1, t);
            var vulcanoPosition = CalculteCoordinates(1000, 5,t);
            var weather = DeterminateWheater(betasoidePosition, ferengiePosition, vulcanoPosition);
            Console.WriteLine(weather);
            return weather;
        }

        private string DeterminateWheater(Point p1, Point p2, Point p3)
        {
            double b = 0;
            var m1 = (p2.Y - p1.Y) / (p2.X - p1.X);
            var m2 = (p3.Y - p1.Y) / (p3.X - p1.X);

            if(m1 == m2)
            {
                b = p1.Y - m1 * p1.X;
                // para por el centro
                if(b == 0)
                {
                    // Sequia
                    return "Sequia";
                }
                else
                {
                    // Condiciones optimas de presion y temperatura
                    return "Condiciones ideales";
                }
            }
            else
            {
                //triangulo
                // Si el area del triangulo con los 3 puntos es mayor
                // al area del poligono de 4 vertices contando el centro, 
                // el centro esta dentro del triangulo
                return "Lluvia";
                // encontrar el maximo dia 
            }
        }

        private Point CalculteCoordinates(int distance, double angularVelocity, double t)
        {
            // necesitp saber la pos anterior
            var point = new Point();
            point.X = Convert.ToInt32(0 + distance * Math.Cos(angularVelocity * t));
            point.Y = Convert.ToInt32(0 + distance * Math.Sin(angularVelocity * t));

            return point;
        }
    }
}
