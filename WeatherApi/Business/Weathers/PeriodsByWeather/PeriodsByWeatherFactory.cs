using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApi.Business.Weathers.Contexts;

namespace WeatherApi.Business.Weathers.PeriodsByWeather
{
    public class PeriodsByWeatherFactory
    {
        public IList<PeriodsByWeather> Create(PlanetCalculationContext planetCalculationContext)
        {
            var list = new List<PeriodsByWeather>();
            list.Add(new PeriodsByWeather()
            {
                Planet = planetCalculationContext.Planet,
                TotalPeriods = planetCalculationContext.GetCurrentPeriodsBy(WeatherType.Drought),
                Weather = "Sequia"
            });
            list.Add(new PeriodsByWeather()
            {
                Planet = planetCalculationContext.Planet,
                TotalPeriods = planetCalculationContext.GetCurrentPeriodsBy(WeatherType.IdealConditions),
                Weather = "Condiciones ideales"
            });
            list.Add(new PeriodsByWeather()
            {
                Planet = planetCalculationContext.Planet,
                TotalPeriods = planetCalculationContext.GetCurrentPeriodsBy(WeatherType.Rainy),
                Weather = "Lluvia"
            });

            return list;
        }
    }
}
