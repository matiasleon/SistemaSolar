using System;
namespace WeatherPredictionMachine.Weathers
{
    public class WeatherResults
    {
        public int DroughtCountDays { get; set; }

        public int RainyCountDays { get; set; }

        public int IdealConditionsCountDays { get; set; }

        public WeatherResults()
        {
            DroughtCountDays = 0;
            RainyCountDays = 0;
            IdealConditionsCountDays = 0;
        }
    }
}
