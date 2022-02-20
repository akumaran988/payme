using System;
using System.Threading.Tasks;
using Com.PaymentApi.Helpers;
using Com.PaymentApi.Models;
using Com.PaymentApi.Models.Query;
using Com.PaymentApi.Solr;
using Com.PaymentApi.Solr.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Com.PaymentApi.Controllers
{
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(ILogger<PaymentController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("payment/")]
        public async Task<Payment> CreatePayment(PaymentRequest paymentRequest)
        {
            var payment = new Payment()
            {
                Id = Guid.NewGuid().ToString(),
                PaymentAmount = paymentRequest.PaymentAmount,
                PaymentCurrency = paymentRequest.PaymentCurrency,
                PaymentDate = paymentRequest.PaymentDate,
                PaymentType = paymentRequest.PaymentType,
                Comment = paymentRequest.Comment,
                CreationDate = DateTime.Now.ToUniversalTime(),
                NettingKey = paymentRequest.NettingKey
            };
            payment.ModificationDate = payment.CreationDate;

            var json = JsonConvert.SerializeObject(new[] { payment });

            var response = await new SolrClient("new_core3", _logger)
                .UpdateAsync(json, "?boost=1.0&commitWithin=1000&overwrite=true&wt=json")
                .ConfigureAwait(false);

            return payment;
        }

        [HttpPost]
        [Route("randompayment/{count}/")]
        public async Task CreateRandomPayment([FromRoute] int count, [FromBody] PaymentRequest paymentRequest)
        {
            var payment = new Payment()
            {
                Id = Guid.NewGuid().ToString(),
                PaymentAmount = paymentRequest.PaymentAmount,
                PaymentCurrency = paymentRequest.PaymentCurrency,
                PaymentDate = paymentRequest.PaymentDate,
                PaymentType = paymentRequest.PaymentType,
                Comment = paymentRequest.Comment,
                CreationDate = DateTime.Now.ToUniversalTime(),
                NettingKey = paymentRequest.NettingKey
            };

            while (count > 0)
            {
                payment.Id = Guid.NewGuid().ToString();
                payment.ModificationDate = DateTime.Now.ToUniversalTime();
                payment.PaymentAmount = new Random().NextDouble() * 100000;
                var json = JsonConvert.SerializeObject(new[] { payment });

                var response = await new SolrClient("new_core3", _logger)
                    .UpdateAsync(json, "?boost=1.0&commitWithin=1000&overwrite=true&wt=json")
                    .ConfigureAwait(false);

                count--;
            }
        }


        [HttpGet]
        [Route("payment/")]
        public async Task<string> GetPayment(string paymentId)
        {
            var query = $"fq=id:{paymentId}&q=*:*&indent=on&wt=json";

            var response = await new SolrClient("new_core3", _logger)
                .SelectAsync(query)
                .ConfigureAwait(false);

            return response;
        }

        [HttpPost]
        [Route("searchsolr/")]
        public async Task<string> SearchSolr(DslQuery query)
        {
            var queryBuilder = new SolrQueryBuilder(query);

            var response = await new SolrClient("new_core3", _logger)
                .SelectAsync(queryBuilder.Build())
                .ConfigureAwait(false);

            return response;
        }
    }
}
