using System;
using System.Collections.Generic;
using WeatherPredictionMachine.Weathers;

namespace WeatherPredictionMachine.Business.Weathers
{
    public class PredictionResults
    {
        public IEnumerable<WeatherByDay>  WeatherByDay { get; set; }

        public PredictionResults()
        {
        }
    }
}
