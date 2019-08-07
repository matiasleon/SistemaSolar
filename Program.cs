using System;
using WeatherPredictionMachine.Business;

namespace WeatherPredictionMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var results = new WeatherMachine().Predict();
            foreach (var weather in results)
            {
                Console.WriteLine(String.Format("Dia:{0} clima:{1}", weather.Day, weather.Weather));
            }
        }
    }
}
