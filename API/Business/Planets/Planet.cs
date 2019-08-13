using System;
using API.Business.Commons;
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
        public double Period => Math.Abs(Math.PI/AngularVelocity).Round(0);

        public Planet(string name, int distanceToSun, int angularVelocity)
        {
            Name = name;
            DistanceToSun = distanceToSun;
            AngularVelocity = angularVelocity;
        }
    }
}
