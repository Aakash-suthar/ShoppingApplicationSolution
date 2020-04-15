using System;
using System.Collections.Generic;

namespace ProductApi.model
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Productname { get; set; }
        public string Productdesciption { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public string Imgurl { get; set; }
    }
}
