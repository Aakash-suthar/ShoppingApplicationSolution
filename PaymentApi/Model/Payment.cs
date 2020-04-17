using System;
using System.Collections.Generic;

namespace PaymentApi.Model
{
    public partial class Payment
    {
        public int Id { get; set; }
        public DateTime? Paymenttime { get; set; }
        public bool? Paymentstatus { get; set; }
        public string Creditnumber { get; set; }
    }
}
