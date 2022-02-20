using System.Collections.Generic;

namespace Com.PaymentApi.Models.Query
{
    public class Pivot
    {
        public List<string> Fields { get; set; }

        public List<LocalParameter> LocalParameters { get; set; }
    }
}