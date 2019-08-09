using System;
using WeatherApi.Planets;

namespace WeatherApi.Business.Weathers
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
