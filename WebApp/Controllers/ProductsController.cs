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
        public ActionResult Payment()
        {
            ViewBag.cartid = Convert.ToInt32(Request.Form["counter"]);
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Paymentpost()
        {
            Orders o = new Orders();
            Cart ct;
            Payment p;
            var token = await HttpContext.GetTokenAsync("access_token");
          

            using (var client = new HttpClient())
            {
                p = new Payment();
                p.Paymentstatus = true;
                p.Creditnumber = Request.Form["creditnumber"];

                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("https://localhost:44322/api/payments");
                var postTask = await client.PostAsJsonAsync<Payment>("payments", p);
                var result = await postTask.Content.ReadAsStringAsync();

                p = JsonConvert.DeserializeObject<Payment>(result);

            }
                using (var client = new HttpClient())
            {
                int counterid = Convert.ToInt32(Request.Form["counter"]);
                ct = c.Where(p => p.id == counterid).Single();

                o.Productid = ct.product.Id;
                o.Quantity = ct.Quantitys;
                o.Totalcost = ct.totalprice;
                o.Userid = User.Claims.Where(p => p.Type == "sub").Select(p => p.Value).Single();
                o.Orderstatus = true;
                o.Email = User.Claims.Where(p => p.Type == "email").Select(p => p.Value).Single();
                o.Paymentid = p.Id;

                o.Adress = Request.Form["adress"];


                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("https://localhost:44321/api/orders");

                var postTask = client.PostAsJsonAsync<Orders>("orders", o);
                postTask.Wait();

                var result = postTask.Result;
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

            if (User.Claims.Where(p => p.Type == "name").Select(p => p.Value).Single() != "admin")
            {
                return RedirectToAction(nameof(Index));
            }

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
            if (User.Claims.Where(p => p.Type == "name").Select(p => p.Value).Single() != "admin")
            {
                return RedirectToAction(nameof(Index));
            }
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
            if (User.Claims.Where(p => p.Type == "name").Select(p => p.Value).Single() != "admin")
            {
                return RedirectToAction(nameof(Index));
            }
            if (id != product.Id)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                client.DefaultRequestHeaders.Authorization =
               new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("https://localhost:44302/api/products");
                var putTask =  client.PutAsJsonAsync<Product>("products", product);
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


        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
