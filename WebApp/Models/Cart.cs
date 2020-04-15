using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Cart
    {
        public Product product;
        public int Quantitys;
        public decimal? totalprice { get { return Quantitys * product.Price; } }
    }
        
       
}
