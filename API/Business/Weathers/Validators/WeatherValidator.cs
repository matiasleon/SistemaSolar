using System;
using API.Business.Commons;
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

        public Weather DeterminateWheater(Point p1, Point p2, Point p3)
        {
            var origin = new Point() { X = 0, Y = 0 };
            var triangleArea = this.geometricCalculator.PoligonArea(p1 ,p2, p3);
            var areaWithOrigin = this.geometricCalculator.PoligonArea(p1, p2, p3, origin);
            if (ArePlanetsAligned(triangleArea))
            {
                if (ThereIsDrought(areaWithOrigin))
                {

                    return new Weather("Sequia", WeatherType.Drought);
                }

                return new Weather("condiciones ideales", WeatherType.IdealConditions);
            }

            if (IsRainyDay(triangleArea, areaWithOrigin))
            {
                return new Weather("LLuvia", WeatherType.Rainy);
            }

            return new Weather("No definido", WeatherType.NotDefined);
        }

        private bool IsRainyDay(double triangleArea, double areaWithOrigin)
        {
            return triangleArea > areaWithOrigin;
        }

        private bool ThereIsDrought(double areaWithOrigin)
        {
            return areaWithOrigin < 10000;
        }

        private bool ArePlanetsAligned(double triangleArea)
        {
            return triangleArea < 10000;  
        }
    }   
}
