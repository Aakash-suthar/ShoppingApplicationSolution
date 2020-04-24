using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.model;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly shoppingdbContext _context;

        public ProductsController(shoppingdbContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable> GetProducts()
        {
            return  _context.Product.ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Product GetProducts(int id)
        {
            Product p = _context.Product.Find(id);
            return p;
        }

        // POST api/<controller>
       // [Authorize]
        [HttpPost]
        public ActionResult PostProducts(Product products)
        {
            _context.Product.Add(products);
            _context.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<controller>/5
      //  [Authorize]
        [HttpPut()]
        public IActionResult PutProducts(Product products)
        {
            Product p = _context.Product.Find(products.Id);

            if (p != null)
            {
                p.Productname = products.Productname;
                p.Productdesciption = products.Productname;
                p.Quantity = products.Quantity;
                p.Imgurl = products.Imgurl;
                p.Price = products.Price;

                _context.SaveChanges();
            }
            else { 
                return BadRequest();
            }

            return Ok();
        }

        // DELETE api/<controller>/5
     //   [Authorize]
        [HttpDelete("{id}")]
        public ActionResult DeleteProducts(int id)
        {
            var products = _context.Product.Find(id);
            if (products == null)
            {
                return NotFound();
            }

            _context.Product.Remove(products);
             _context.SaveChanges();

            return Ok();
        }

        private bool ProductsExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
