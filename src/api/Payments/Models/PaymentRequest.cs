using System;

namespace Com.PaymentApi.Models
{
    public class PaymentRequest
    {
        /// <summary>
        /// Payment date
        /// </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Actual amount to be payed
        /// </summary>
        public double PaymentAmount { get; set; }

        /// <summary>
        /// Actual amount to be payed
        /// </summary>
        public string PaymentCurrency { get; set; }

        /// <summary>
        /// Type of the payment <cref>PaymentType</cref>
        /// </summary>
        public PaymentType PaymentType { get; set; }

        /// <summary>
        /// User comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// A key used to net payments
        /// </summary>
        public string NettingKey { get; set; }
    }
}
