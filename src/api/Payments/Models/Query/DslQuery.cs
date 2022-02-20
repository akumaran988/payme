using System.Collections.Generic;

namespace Com.PaymentApi.Models.Query
{
    public class DslQuery
    {
        public int Start { get; set; }

        public int Row { get; set; }

        public List<string> Query { get; set; }

        public List<string> FilterQuery { get; set; }

        public Facets Facets { get; set; }

        public List<Stats> Stats { get; set; }

        public List<string> Fields { get; set; }
    }
}
