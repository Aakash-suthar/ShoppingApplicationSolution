using System;
using System.Collections.Generic;

namespace PaymentApi.model
{
    public partial class Payment
    {
        public int Id { get; set; }
        public int? Orderid { get; set; }
        public DateTime? Paymenttime { get; set; }
        public bool? Paymentstatus { get; set; }
    }
}
