using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class Orders
    {
        public int Id { get; set; }
        public int? Productid { get; set; }
        public int? Quantity { get; set; }
        public decimal? Totalcost { get; set; }
        public DateTime? Ordertime { get; set; }
        public bool? Orderstatus { get; set; }
        public string Userid { get; set; }
        public int? Paymentid { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
    }
}
