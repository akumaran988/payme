using System.Collections.Generic;

namespace Com.PaymentApi.Models.Query
{
    public class Facets
    {
        public int Limit { get; set; }

        public int PivotMinCount { get; set; }

        public List<Pivot> Pivots { get; set; }
    }
}