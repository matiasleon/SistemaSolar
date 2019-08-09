
namespace WeatherApi.Weathers
{
    public class WeatherByDay
    {
        public string Weather { get; set; }

        public double Day { get; set; }

        public WeatherByDay(string weather, double day)
        {
            Weather = weather;
            Day = day;
        }
    }
}
