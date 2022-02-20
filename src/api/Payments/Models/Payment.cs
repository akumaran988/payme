using System;
using Newtonsoft.Json;

namespace Com.PaymentApi.Models
{
    public class Payment
    {
        /// <summary>
        /// Unique Id for payments
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Payment date
        /// </summary>
        [JsonProperty("paymentdate")]
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Actual amount to be payed
        /// </summary>
        [JsonProperty("paymentamount")]
        public double PaymentAmount { get; set; }

        /// <summary>
        /// Actual amount to be payed
        /// </summary>
        [JsonProperty("paymentcurrency")]
        public string PaymentCurrency { get; set; }

        /// <summary>
        /// Type of the payment <cref>PaymentType</cref>
        /// </summary>
        [JsonProperty("paymenttype")]
        public PaymentType PaymentType { get; set; }

        /// <summary>
        /// User comment
        /// </summary>
        [JsonProperty("comment")]
        public string Comment { get; set; }

        /// <summary>
        /// Modification date of the payment
        /// </summary>
        [JsonProperty("modificationdate")]
        public DateTime ModificationDate { get; set; }

        /// <summary>
        /// Creation date of the payment
        /// </summary>
        [JsonProperty("creationdate")]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// A key used to net payments
        /// </summary>
        [JsonProperty("nettingkey")]
        public string NettingKey { get; set; }

        [JsonProperty("dateasstringversion")]
        public string Version
        {
            get => ModificationDate.Ticks.ToString();
        }
    }
}
