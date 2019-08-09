using System;
using System.Collections.Generic;
using WeatherApi.Weathers;

namespace WeatherApi.Business.Weathers
{
    public class PredictionResults
    {
        public IEnumerable<WeatherByDay>  WeatherByDay { get; set; }

        public PredictionResults()
        {
        }
    }
}
