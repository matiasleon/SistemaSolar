using System;
using System.Collections.Generic;
using API.Planets;

namespace API.Business.Weathers.Contexts
{
    public class PlanetCalculationContext
    {
       public Planet Planet { get; private set; }
     
        private Dictionary<WeatherType, int> OccurrencesByWeather = new Dictionary<WeatherType, int>();

        private int DaysPerPeriodTracking { get; set; }

        private int DaysPerPeriodWithSameWeatherTracking { get; set; }

        private WeatherType LastWeather { get; set; }

        public PlanetCalculationContext(Planet planet)
        {
            Planet = planet;
            DaysPerPeriodTracking = 0;
            OccurrencesByWeather.Add(WeatherType.Drought, 0);
            OccurrencesByWeather.Add(WeatherType.Rainy, 0);
            OccurrencesByWeather.Add(WeatherType.IdealConditions, 0);
            OccurrencesByWeather.Add(WeatherType.NotDefined, 0);
        }

        private void SetOcurrence(WeatherType weatherType)
        {
            OccurrencesByWeather[weatherType]++;
        }

        public int GetCurrentPeriodsBy(WeatherType weathertype)
        {
            return OccurrencesByWeather[weathertype];
        }

        public void UpdateContext(WeatherType weatherType)
        {
            DaysPerPeriodTracking++;

            if (DaysPerPeriodTracking <= Planet.Period)
            {
                if (weatherType == LastWeather)
                {
                    DaysPerPeriodWithSameWeatherTracking++;
                }
                else
                {
                    DaysPerPeriodWithSameWeatherTracking = 0;
                }
            }
            else
            {
                DaysPerPeriodTracking = 0;
                if (DaysPerPeriodWithSameWeatherTracking == Planet.Period)
                {
                    SetOcurrence(weatherType);
                }
            }
            LastWeather = weatherType;  
        }
    }
}
