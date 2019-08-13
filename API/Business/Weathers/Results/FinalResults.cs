using API.Business.Weathers.PeriodsByWeathers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Business.Weathers.Results
{
    public class FinalResults
    {
        public IEnumerable<PeriodsByWeather> PeriodsByWeather { get; set; } 

        public int RainyMaxValueDate { get; set; }
    }
}
