﻿using System;
using System.Collections.Generic;
using WeatherPredictionMachine.Planets;
using WeatherPredictionMachine.Weathers;

namespace WeatherPredictionMachine.Business.Weathers.Contexts
{
    public class PlanetCalculationContext
    {
        private Dictionary<WeatherType, int> OccurrencesByWeather = new Dictionary<WeatherType, int>();

        public Planet Planet { get; set; }

        public int DaysPerPeriodTracking { get; set; }

        public int DaysPerPeriodWithSameWeatherTracking { get; set; }

        public WeatherType LastWeather { get; set; }

        public int TotalPeriods { get; set; }

        public List<PeriodsByWeather> PeriodsByWeather { get; set; }

        public PlanetCalculationContext(Planet planet)
        {
            Planet = planet;
            OccurrencesByWeather.Add(WeatherType.Drought, 0);
            OccurrencesByWeather.Add(WeatherType.Rainy, 0);
            OccurrencesByWeather.Add(WeatherType.IdealConditions, 0);
        }

        public void SetOcurrence(WeatherType weatherType)
        {
            OccurrencesByWeather[weatherType]++;
        }

        public void ShowResults()
        {

        }
    }
}
