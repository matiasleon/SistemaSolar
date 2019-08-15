
namespace API.Business.Weathers.Results
{
    public class Prediction
    {
        public int DroughtPeriods { get; set; }

        public int RainyPeriods { get; set; }

        public int IdealConditionsPeriods { get; set; }

        public int RainyMaxValueDate { get; set; }

        public Prediction(int droughtPeriods,
                          int rainyPeriods,
                          int idealConditonsPeriods,
                          int rainyMaxValueDate)
        {
            this.DroughtPeriods = droughtPeriods;
            this.RainyPeriods = rainyPeriods;
            this.IdealConditionsPeriods = idealConditonsPeriods;
            this.RainyMaxValueDate = rainyMaxValueDate;
        }
    }
}
