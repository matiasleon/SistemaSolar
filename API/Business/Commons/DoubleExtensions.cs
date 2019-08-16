using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Business.Commons
{
    public static class DoubleExtensions
    {
        public static double Round(this double number, int digits = 2)
        {
            return Math.Round(number, digits, MidpointRounding.AwayFromZero);
        }
    }
}
