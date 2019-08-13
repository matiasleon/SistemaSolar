using System;
using API.Business.Weathers.Calculators;
using API.Commons;

namespace API.Business.Weathers.Validators
{
    public class WeatherValidator
    {
        private readonly GeometricCalculator geometricCalculator;

        public WeatherValidator(GeometricCalculator geometricCalculator)
        {
            this.geometricCalculator = geometricCalculator;
        }

        public bool IsRainyDay(Point p1, Point p2, Point p3)
        {
            var originPoint = new Point { X = 0, Y = 0 };
            return geometricCalculator.PoligonArea(new Point[] { p1, p2, p3 }) > geometricCalculator.PoligonArea(new Point[] { p1, p2, p3, originPoint });
        }

        public bool ThereIsDrought(Point p1, Point p2)
        {
            var m = (p2.Y - p1.Y) / (p2.X - p1.X);
            var independentConstant = p1.Y - m * p1.X;
            return independentConstant == 0;
        }

        public Weather DeterminateWheater(Point p1, Point p2, Point p3)
        {
            if (geometricCalculator.ArePlanetsAligned(p1, p2, p3))
            {
                if (ThereIsDrought(p1, p2))
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
    }
}
