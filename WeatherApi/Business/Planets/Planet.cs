using System;
using WeatherApi.Commons;

namespace WeatherApi.Planets
{
    public class Planet
    {
        public double AngularVelocity { get; set; }

        public double DistanceToSun { get; set; }

        public string Description { get; set; }

        public Point Point { get; set; }

        /// <summary>
        /// Unidad de medida es el dia
        /// </summary>
        public int Period => Convert.ToInt32(Math.PI/AngularVelocity);
    }
}
