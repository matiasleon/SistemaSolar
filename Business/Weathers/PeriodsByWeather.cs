using System;
using WeatherPredictionMachine.Planets;

namespace WeatherPredictionMachine.Business.Weathers
{
    public class PeriodsByWeather
    {
        public Planet Planet { get; set; }

        public int PeriodsCount { get; set;}

        public int PlanentDaysByPeriod => Planet.Period;

        public PeriodsByWeather(Planet planet)
        {
            Planet = planet;
        }
    }
}
