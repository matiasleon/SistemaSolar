using System;
using WeatherApi.Weathers;

namespace WeatherApi.Business.Weather
{
    public class Weather
    {
        public string Name { get; set; }

        public WeatherType Type { get; set; }

        public Weather(string name, WeatherType type)
        {
            Name = name;
            Type = type;
        }
    }
}
