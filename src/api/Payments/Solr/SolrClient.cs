using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Com.PaymentApi.Solr
{
    public class SolrClient
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly ILogger _logger;
        private readonly string _datasource;
        private string _baseuri = @"http://localhost:8983/solr/";

        public SolrClient(string dataSource, ILogger logger)
        {
            _logger = logger;
            _datasource = dataSource;
         }

        public SolrClient RegisterConnectionBaseUri(string baseUri)
        {
            _baseuri = baseUri;
            return this;
        }

        public async Task<string> SelectAsync(string query)
        {
            var uri = new Uri(_baseuri + $"{_datasource}/" + $"select?{query}");
            var request = HttpWebRequest.Create(uri);
            request.Method = WebRequestMethods.Http.Get;
            request.AuthenticationLevel = AuthenticationLevel.None;

            try
            {
                using (var response = request.GetResponse())
                {
                    var reader = new StreamReader(response.GetResponseStream());
                    return await reader.ReadToEndAsync();
                }
            }
            catch(Exception exception)
            {
                _logger.LogError($"Failed request {JsonConvert.SerializeObject(request)} {nameof(exception)} : {exception}");
                throw;
            }
        }

        public async Task<HttpResponseMessage> UpdateAsync(string content, string requestParameters)
        {
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            
            try
            {
                return await _httpClient
                    .PostAsync(_baseuri + $"{_datasource}/" + $"update" + requestParameters, httpContent);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Failed Request for {nameof(content)}:{content}, {nameof(exception)} : {exception}");
                throw;
            }
        }
    }
}
