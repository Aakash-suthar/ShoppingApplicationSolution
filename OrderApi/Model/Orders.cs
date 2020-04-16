using System;
using System.Collections.Generic;

namespace OrderApi.Model
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
    }
}
