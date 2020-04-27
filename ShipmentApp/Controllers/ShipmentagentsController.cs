using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShipmentApp.Data;
using ShipmentApp.Models;
namespace ShipmentApp.Controllers
{
  ///  [Authorize]
    public class ShipmentagentsController : Controller
    {
        private readonly ShipmentAppContext _context;

        public ShipmentagentsController(ShipmentAppContext context)
        {
            _context = context;
        }

        // GET: Shipmentagents
        public async Task<IActionResult> Index()
        {
            List<Shipmentagent> ol;
            using (var client = new HttpClient())
            {
               /* var token = await HttpContext.GetTokenAsync("access_token");
                client.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", token);*/
                var data = await (await client.GetAsync($"https://shipmentapi2.azurewebsites.net/api/shipmentagents")).Content.ReadAsStringAsync();
                ol = JsonConvert.DeserializeObject<List<Shipmentagent>>(data);

            }
            return View(ol);
        }

        // GET: Shipmentagents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipmentagent = await _context.Shipmentagent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipmentagent == null)
            {
                return NotFound();
            }

            return View(shipmentagent);
        }

        // GET: Shipmentagents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shipmentagents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Orderid,Orderplacedate,Deliverydate,DeliveryGuy,Statuss")] Shipmentagent shipmentagent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shipmentagent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shipmentagent);
        }

        // GET: Shipmentagents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipmentagent = await _context.Shipmentagent.FindAsync(id);
            if (shipmentagent == null)
            {
                return NotFound();
            }
            return View(shipmentagent);
        }

        // POST: Shipmentagents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Orderid,Orderplacedate,Deliverydate,DeliveryGuy,Statuss")] Shipmentagent shipmentagent)
        {
            if (id != shipmentagent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipmentagent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipmentagentExists(shipmentagent.Id))
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
            return View(shipmentagent);
        }

        // GET: Shipmentagents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipmentagent = await _context.Shipmentagent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipmentagent == null)
            {
                return NotFound();
            }

            return View(shipmentagent);
        }

        // POST: Shipmentagents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shipmentagent = await _context.Shipmentagent.FindAsync(id);
            _context.Shipmentagent.Remove(shipmentagent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShipmentagentExists(int id)
        {
            return _context.Shipmentagent.Any(e => e.Id == id);
        }
    }
}
