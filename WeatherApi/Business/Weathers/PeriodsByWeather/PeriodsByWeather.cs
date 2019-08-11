﻿using System;
using WeatherApi.Planets;

namespace WeatherApi.Business.Weathers.PeriodsByWeather
{
    public class PeriodsByWeather
    {
        public Planet Planet { get; set; }

        public int TotalPeriods { get; set;}

        public string Weather { get; set; }
    }
}