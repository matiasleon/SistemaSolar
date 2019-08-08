using System;
namespace WeatherPredictionMachine.Weathers.Updaters
{
    public class WeatherCountUpdater
    {
        public void Update(WeatherResults countByWeather, WeatherType weatherType)
        {
            switch (weatherType)
            {
                case WeatherType.IdealConditions:
                    countByWeather.IdealConditionsCountDays++;
                    break;
                case WeatherType.Rainy:
                    countByWeather.RainyCountDays++;
                    break;
                case WeatherType.Drought:
                    countByWeather.DroughtCountDays++;
                    break;
                default:
                    break;
            }
        }
    }
}
