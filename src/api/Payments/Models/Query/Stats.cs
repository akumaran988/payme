using System.Collections.Generic;

namespace Com.PaymentApi.Models.Query
{
    public class Stats
    {
        public string Field { get; set; }

        public List<LocalParameter> LocalParameters { get; set; }
    }
}