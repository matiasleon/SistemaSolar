using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApi.Dtos
{
    public class PredictionDto
    {
        public int Periods { get; set; }

        public string Planet { get; set; }

        public string Weather { get; set; }
    }
}
