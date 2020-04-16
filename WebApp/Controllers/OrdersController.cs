using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly WebAppContext _context;

        public OrdersController(WebAppContext context)
        {
            _context = context;
        }

        // GET: Orders

        public async Task<IActionResult> Index()
        {
            /*return View(await _context.Orders.ToListAsync());*/
            List<Orders> ol;
            var token = await HttpContext.GetTokenAsync("access_token");
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
                var data = await (await client.GetAsync($"https://localhost:44321/api/orders")).Content.ReadAsStringAsync();
                ol = JsonConvert.DeserializeObject<List<Orders>>(data);
                string id = User.Claims.Where(p => p.Type == "sub").Select(p => p.Value).Single();
                ol = ol.Where(p => p.Userid == id).ToList();


            }
            using (var client = new HttpClient())
             {
                var data = await (await client.GetAsync($"https://localhost:44302/api/products")).Content.ReadAsStringAsync();
                List<Product> pl = JsonConvert.DeserializeObject<List<Product>>(data);
                ViewBag.productlist = pl;
                return View(ol);
            }
            }

        // GET: Orders/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var token = await HttpContext.GetTokenAsync("access_token");
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
                var data = await (await client.GetAsync($"https://localhost:44321/api/orders/"+id)).Content.ReadAsStringAsync();
                if (data == null)
                {
                    return NotFound();
                }
                Orders order = JsonConvert.DeserializeObject<Orders>(data);
                return View(order);

            }
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Productid,Quantity,Totalcost,Ordertime,Orderstatus")] Orders orders)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("https://localhost:44321/api/orders");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Orders>("order", orders);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            }
            return View(orders);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Productid,Quantity,Totalcost,Ordertime,Orderstatus")] Orders orders)
        {
            if (id != orders.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(orders);
        }

        // GET: Orders/Delete/5
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
                var data = await (await client.GetAsync($"https://localhost:44321/api/orders")).Content.ReadAsStringAsync();
                List<Orders> ol = JsonConvert.DeserializeObject<List<Orders>>(data);
                Orders order = ol.Find(m => m.Id == id);
           
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
            }
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            using (var client = new HttpClient())
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                client.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", token);

                client.BaseAddress = new Uri("https://localhost:44321/api/");
                var deleteTask = client.DeleteAsync("orders/" + id.ToString());
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
