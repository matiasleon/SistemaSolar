using System;
using WeatherPredictionMachine.Commons;

namespace WeatherPredictionMachine.Planets
{
    public class Planet
    {
        public double AngularVelocity { get; set; }

        public double DistanceToSun { get; set; }

        public string Description { get; set; }

        public Point Point { get; set; }

        public int Period => Convert.ToInt32(Math.PI/AngularVelocity);
    }
}
