using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.PaymentApi.Models.Query;

namespace Com.PaymentApi.Solr.Query
{
    public class SolrQueryBuilder
    {
        private readonly DslQuery _dslQuery;

        public SolrQueryBuilder(DslQuery dslQuery)
        {
            _dslQuery = dslQuery;
        }

        public string Build()
        {
            var query = new StringBuilder();

            var q = _dslQuery.Query;

            if (q != null)
            {
                query.Append("&q=");
                EnrichQueryWithRawQueryParameters(query, q);
            }

            var fq = _dslQuery.FilterQuery;

            if (fq != null)
            {
                query.Append("&fq=");
                EnrichQueryWithRawQueryParameters(query, fq);
            }

            var fields = _dslQuery.Fields;
            if (fields != null)
            {
                query.Append("&fl=");
                EnrichQueryWithRawQueryParameters(query, fields);
            }

            var facets = _dslQuery.Facets;

            if(facets != null)
            {
                EnrichQueryWithFacets(query, facets);
            }

            var stats = _dslQuery.Stats;

            if (stats != null)
            {
                EnrichQueryWithStatsFields(query, stats);
            }

            
            query.Append("&start=");
            query.Append(_dslQuery.Start);

            query.Append("&rows=");
            query.Append(_dslQuery.Row);

            query.Append("&wt=json&indent=on");

            return query.ToString();
        }

        private static void EnrichQueryWithRawQueryParameters(StringBuilder query, List<string> queryParameters)
        {
            for (var index = 0; index < queryParameters.Count(); index++)
            {
                query.Append(queryParameters[index]);

                if (index < queryParameters.Count() - 1)
                {
                    query.Append("&");
                }
            }
        }

        private static void EnrichQueryWithFacets(StringBuilder query, Facets facets)
        {
            query.Append("&");
            query.Append("facet=true");
            var pivots = facets.Pivots;

            for (var index = 0; index < pivots.Count(); index++)
            {
                var pivot = pivots[index];
                if (pivot != null)
                {
                    query.Append("&");
                    query.Append("facet.pivot=");
                    if (pivot.LocalParameters?.Any() ?? false)
                    {
                        EnrichWithLocalParameters(query, pivot.LocalParameters);
                    }
                    query.Append(string.Join(",", pivot.Fields));
                }
            }

            if (facets.Limit > 0)
            {
                query.Append("&");
                query.Append("facet.limit=");
                query.Append(facets.Limit);
            }

            if (facets.PivotMinCount > 0)
            {
                query.Append("&");
                query.Append("facet.pivot.mincount");
                query.Append(facets.PivotMinCount);
            }
        }

        private static void EnrichQueryWithStatsFields(StringBuilder query, List<Stats> stats)
        {
            query.Append("&");
            query.Append("stats=true");
            foreach (var stat in stats)
            {
                query.Append("&");
                query.Append($"stats.field=");
                if (stat.LocalParameters?.Any() ?? false)
                {
                    EnrichWithLocalParameters(query, stat.LocalParameters);
                }
                query.Append(stat.Field);
            }
        }

        private static void EnrichWithLocalParameters(StringBuilder query, List<LocalParameter> localParameters)
        {
            var isFirstValue = true;
            query.Append("{");

            foreach (var localParameter in localParameters)
            {
                if (!isFirstValue)
                {
                    query.Append(" ");
                }
                query.Append(localParameter.LocalParamKey);
                query.Append("=");
                query.Append(localParameter.LocalParamValue);
                isFirstValue = false;
            }

            query.Append("}");
        }
    }
}
