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

        public Weather DeterminateWheater(double areaOfPlanets, double areaOfPlanetsWithOrigin)
        {
            if (ArePlanetsAligned(areaOfPlanets))
            {
                if (ThereIsDrought(areaOfPlanetsWithOrigin))
                {
                    // Sequia
                    return new Weather("Sequia", WeatherType.Drought);
                }
                // Condiciones optimas de presion y temperatura
                return new Weather("condiciones ideales", WeatherType.IdealConditions);
            }

            if (IsRainyDay(areaOfPlanets, areaOfPlanetsWithOrigin))
            {
                return new Weather("LLuvia", WeatherType.Rainy);
            }

            return new Weather("Incorrecto", WeatherType.NotDefined);
        }

        private bool IsRainyDay(double areaOfPlanets, double areaOfPlanetsWithOrigin)
        {
            return areaOfPlanets > areaOfPlanetsWithOrigin;
        }

        private bool ThereIsDrought(double areaOfPlanetsWithOrigin)
        {

            //var m = (p2.Y - p1.Y) / (p2.X - p1.X);
            //var independentConstant = p1.Y - m * p1.X;
            //return independentConstant == 0;

            return areaOfPlanetsWithOrigin == 0;
        }

        private bool ArePlanetsAligned(double area)
        {
            // utilizar el area, si es igual a cero
            //var m1 = (p2.Y - referencePoint.Y) / (p2.X - referencePoint.X);
            //var m2 = (p3.Y - referencePoint.Y) / (p3.X - referencePoint.X);

            //return m1 == m2;
            return area == 0;
        }
    }
}
