using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class Payment
    {
        public int Id { get; set; }
        public int? Orderid { get; set; }
        public DateTime? Paymenttime { get; set; }
        public bool? Paymentstatus { get; set; }
    }
}
