using System;
using API.Business.Commons;
using API.Commons;

namespace API.Planets
{
    public class Planet
    {
        public int Id { get; set; }

        public int AngularVelocity { get; set; }

        public int DistanceToSun { get; set; }

        public string Name { get; set; }

        public Planet(string name, int distanceToSun, int angularVelocity)
        {
            Name = name;
            DistanceToSun = distanceToSun;
            AngularVelocity = angularVelocity;
        }
    }
}
