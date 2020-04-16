using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace WebApp.Controllers
{
    public class ProductsController : Controller
    {
        static int counter = 0;
        static List<Cart> c = new List<Cart>();

        #region dummy data
        /* List<Product> oltp = new List<Product>() {
             new Product(){ Id=1, Productname="akash",Price=23, Productdesciption="jqwvhdbqbdjqvwdjqvjwdvqjvwjdq",Imgurl="kqwbjdqwdqbwdqwdkqbwd",Quantity=3 },
             new Product(){ Id=2, Productname="akash2",Price=23, Productdesciption="jqwvhdbqbdjqvwdjqvjwdvqjvwjdq",Imgurl="kqwbjdqwdqbwdqwdkqbwd",Quantity=3 },
             new Product(){ Id=3, Productname="akash3" ,Price=53, Productdesciption="jqwvhdbqbdjqvwdjqvjwdvqjvwjdq",Imgurl="kqwbjdqwdqbwdqwdkqbwd",Quantity=3},
             new Product(){ Id=1, Productname="akash4",Price=26 , Productdesciption="jqwvhdbqbdjqvwdjqvjwdvqjvwjdq",Imgurl="kqwbjdqwdqbwdqwdkqbwd",Quantity=3},
             new Product(){ Id=2, Productname="akash5",Price=27 , Productdesciption="jqwvhdbqbdjqvwdjqvjwdvqjvwjdq",Imgurl="kqwbjdqwdqbwdqwdkqbwd",Quantity=3},
             new Product(){ Id=3, Productname="akash6" ,Price=29, Productdesciption="jqwvhdbqbdjqvwdjqvjwdvqjvwjdq",Imgurl="kqwbjdqwdqbwdqwdkqbwd",Quantity=3},

             };*/
        #endregion

        private readonly WebAppContext _context;

        public ProductsController(WebAppContext context)
        {
            _context = context;
        }

        #region Cart methods
        [HttpGet]
        public ActionResult ViewCart()
        {
            ViewBag.cartlist = c;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Order()
        {
          
             var token = await HttpContext.GetTokenAsync("access_token");
            using (var client = new HttpClient())
            {
                Orders o = new Orders();
                o.Productid = Convert.ToInt32(Request.Form["id"]);
                o.Quantity = Convert.ToInt32(Request.Form["quans"]);
                o.Totalcost = Convert.ToDecimal(Request.Form["tp"]);
                o.Userid = User.Claims.Where(p => p.Type == "sub").Select(p => p.Value).Single();
                    o.Orderstatus = false;
                
                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("https://localhost:44321/api/orders");

                var postTask = client.PostAsJsonAsync<Orders>("orders", o);
                postTask.Wait();

                var result = postTask.Result;
                int counterid = Convert.ToInt32(Request.Form["counter"]);
                Cart ct = c.Where(p => p.id == counterid).Single();
                c.Remove(ct);
                
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<ActionResult> Cart(int id)
        {
            using (var client = new HttpClient())
            {
                var data = await(await client.GetAsync($"https://localhost:44302/api/products")).Content.ReadAsStringAsync();
                List<Product> ol = JsonConvert.DeserializeObject<List<Product>>(data);
                Product product = ol.Find(m => m.Id == id);
                product.Id = id;
                ViewBag.quantity = ol.Find(m => m.Id == id).Quantity;
                return View(product);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Cart()
        {
            using (var client = new HttpClient())
            {
                var data = await (await client.GetAsync($"https://localhost:44302/api/products")).Content.ReadAsStringAsync();
                List<Product> ol = JsonConvert.DeserializeObject<List<Product>>(data);
                Product product = ol.Find(m => m.Id == Convert.ToInt32(Request.Form["id"]));
                product.Id = Convert.ToInt32(Request.Form["id"]);
                c.Add(new Cart()
                {
                    id = counter++,
                    product = product,
                    Quantitys = Convert.ToInt32(Request.Form["Quan"])
                }) ;

                return RedirectToAction(nameof(Index));
            }
        }

        #endregion

        // GET: Products
        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                var data = await (await client.GetAsync($"https://localhost:44302/api/products")).Content.ReadAsStringAsync();
                List<Product> ol = JsonConvert.DeserializeObject<List<Product>>(data);
                return View(ol);
            }

        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (var client = new HttpClient())
            {
                var data = await (await client.GetAsync($"https://localhost:44302/api/products")).Content.ReadAsStringAsync();
                List<Product> ol = JsonConvert.DeserializeObject<List<Product>>(data);
                // return View(ol);
                var product = ol.Find(m => m.Id == id);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }
        }

        // GET: Products/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Productname,Productdesciption,Price,Quantity,Imgurl")] Product product)
        {

            var token = await HttpContext.GetTokenAsync("access_token");
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("https://localhost:44302/api/products");

                var postTask = client.PostAsJsonAsync<Product>("products", product);
                postTask.Wait();

                var result = postTask.Result;
                    return RedirectToAction("Index");
              //  ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            }
        }

        // GET: Products/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (var client = new HttpClient())
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                client.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", token);
                var data = await (await client.GetAsync($"https://localhost:44302/api/products")).Content.ReadAsStringAsync();
                List<Product> ol = JsonConvert.DeserializeObject<List<Product>>(data);
                var product = ol.Find(m => m.Id == id);
               
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
            
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Productname,Productdesciption,Price,Quantity,Imgurl")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

         
            using (var client = new HttpClient())
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                client.DefaultRequestHeaders.Authorization =
               new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("https://localhost:44302/api/products/" + id);
                var putTask =  client.PutAsJsonAsync<Product>("product", product);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else {
                    return View(product);
                }
            }

        }

        // GET: Products/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                client.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", token);
                var data = await (await client.GetAsync($"https://localhost:44302/api/products")).Content.ReadAsStringAsync();
                List<Product> ol = JsonConvert.DeserializeObject<List<Product>>(data);
                var product = ol.Find(m => m.Id == id);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                client.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", token);
                var data = await (await client.GetAsync($"https://localhost:44302/api/products")).Content.ReadAsStringAsync();
                List<Product> ol = JsonConvert.DeserializeObject<List<Product>>(data);
                // return View(ol);
                var product = ol.Find(m => m.Id == id);
            }
           
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
