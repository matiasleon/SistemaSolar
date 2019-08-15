using System;
using API.Business.Weathers.Calculators;
using API.Commons;

namespace API.Business.Weathers.Validators
{
    public class WeatherValidator
    {
        private readonly GeometricCalculator geometricCalculator;

        private const double EPSILON = 1;

        public WeatherValidator(GeometricCalculator geometricCalculator)
        {
            this.geometricCalculator = geometricCalculator;
        }

        //todo: implementar un chaain of responsability in order to hava a cleaner code
        public Weather DeterminateWheater(Point p1, Point p2, Point p3)
        {
            if (ArePlanetsAligned(p1, p2, p3))
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

        private bool IsRainyDay(Point p1, Point p2, Point p3)
        {
            var areaOfPlanets = geometricCalculator.PoligonArea(p1, p2, p3);
            var areaOfPlanetsWithOrigin = geometricCalculator.PoligonArea(p1, p2, p3);

            return areaOfPlanets > areaOfPlanetsWithOrigin;
        }

        private bool ThereIsDrought(Point p1, Point p2)
        {
            var m = (p2.Y - p1.Y) / (p2.X - p1.X);
            var independentConstant = p1.Y - m * p1.X;
            return Math.Abs(independentConstant) < EPSILON;
        }

        private bool ArePlanetsAligned(Point p1, Point p2, Point p3)
        {
            var m1 = (p2.Y - p1.Y) / (p2.X - p1.X);
            var m2 = (p3.Y - p1.Y) / (p3.X - p1.X);

            return Math.Abs(m1 - m2) < EPSILON;
        }
    }
}
