using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShipmentApp.Data;
using ShipmentApp.Models;

namespace ShipmentApp.Controllers
{
    [Authorize]

    public class Shipmentagent3Controller : Controller
    {
        private readonly ShipmentAppContext _context;

        public Shipmentagent3Controller(ShipmentAppContext context)
        {
            _context = context;
        }

        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }

        // GET: Shipmentagent3
        public async Task<IActionResult> Index()
        {
            List<Shipmentagent3> ol;
            using (var client = new HttpClient())
            {
                /* var token = await HttpContext.GetTokenAsync("access_token");
                 client.DefaultRequestHeaders.Authorization =
                  new AuthenticationHeaderValue("Bearer", token);*/
                var data = await (await client.GetAsync($"https://shipmentapinew.azurewebsites.net/api/shipmentagent3")).Content.ReadAsStringAsync();
                ol = JsonConvert.DeserializeObject<List<Shipmentagent3>>(data);

            }
            return View(ol);
        }

        // GET: Shipmentagent3/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipmentagent3 = await _context.Shipmentagent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipmentagent3 == null)
            {
                return NotFound();
            }

            return View(shipmentagent3);
        }

        // GET: Shipmentagent3/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shipmentagent3/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Orderid,DeliveryGuy,Statuss")] Shipmentagent3 shipmentagent3)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shipmentagent3);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shipmentagent3);
        }

        // GET: Shipmentagent3/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            List<Shipmentagent3> ol;
            Shipmentagent3 s = new Shipmentagent3();
            using (var client = new HttpClient())
            {
                var data = await (await client.GetAsync($"https://shipmentapinew.azurewebsites.net/api/shipmentagent3")).Content.ReadAsStringAsync();
                ol = JsonConvert.DeserializeObject<List<Shipmentagent3>>(data);
                s = ol.Where(p => p.Id == id).Single();
            }

            return View(s);
        }

        // POST: Shipmentagent3/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Orderid,DeliveryGuy,Statuss")] Shipmentagent3 shipmentagent3)
        {


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://shipmentapinew.azurewebsites.net/api/shipmentagent3");
                var postTask = await client.PutAsJsonAsync<Shipmentagent3>("shipmentagent3", shipmentagent3);
                //postTask.Wait();
                //  var result = postTask.Result;
            }

            return RedirectToAction(nameof(Index));
          //  }
           // return View(shipmentagent3);
        }

        // GET: Shipmentagent3/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipmentagent3 = await _context.Shipmentagent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipmentagent3 == null)
            {
                return NotFound();
            }

            return View(shipmentagent3);
        }

        // POST: Shipmentagent3/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shipmentagent3 = await _context.Shipmentagent.FindAsync(id);
            _context.Shipmentagent.Remove(shipmentagent3);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Shipmentagent3Exists(int id)
        {
            return _context.Shipmentagent.Any(e => e.Id == id);
        }
    }
}
