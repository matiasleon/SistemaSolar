using System;
using API.Commons;

namespace API.Planets
{
    public class Planet
    {
        public int AngularVelocity { get; set; }

        public int DistanceToSun { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Unidad de medida es el dia
        /// </summary>
        public int Period => Math.Abs(Convert.ToInt32(Math.PI/AngularVelocity));

        public Planet(string name, int distanceToSun, int angularVelocity)
        {
            Name = name;
            DistanceToSun = distanceToSun;
            AngularVelocity = angularVelocity;
        }
    }
}
