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
    [Route("api/[controller]")]
    [ApiController] 
    [Authorize]
    public class ShipmentagentsController : ControllerBase
    {
        private readonly ShipmentAppContext _context;

        public ShipmentagentsController(ShipmentAppContext context)
        {
            _context = context;
        }

        // GET: api/Shipmentagents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shipmentagent>>> GetShipmentagent()
        {
            List<Shipmentagent> ol;
            using (var client = new HttpClient())
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                client.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", token);
                var data = await (await client.GetAsync($"https://localhost:44332/api/Shipmentagents")).Content.ReadAsStringAsync();
                 ol = JsonConvert.DeserializeObject<List<Shipmentagent>>(data);
                
            }


            return ol;
        }

        // GET: api/Shipmentagents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shipmentagent>> GetShipmentagent(int id)
        {
            var shipmentagent = await _context.Shipmentagent.FindAsync(id);

            if (shipmentagent == null)
            {
                return NotFound();
            }

            return shipmentagent;
        }

        // PUT: api/Shipmentagents/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShipmentagent(int id, Shipmentagent shipmentagent)
        {
            if (id != shipmentagent.Id)
            {
                return BadRequest();
            }

            _context.Entry(shipmentagent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShipmentagentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Shipmentagents
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Shipmentagent>> PostShipmentagent(Shipmentagent shipmentagent)
        {
            _context.Shipmentagent.Add(shipmentagent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShipmentagent", new { id = shipmentagent.Id }, shipmentagent);
        }

        // DELETE: api/Shipmentagents/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Shipmentagent>> DeleteShipmentagent(int id)
        {
            var shipmentagent = await _context.Shipmentagent.FindAsync(id);
            if (shipmentagent == null)
            {
                return NotFound();
            }

            _context.Shipmentagent.Remove(shipmentagent);
            await _context.SaveChangesAsync();

            return shipmentagent;
        }

        private bool ShipmentagentExists(int id)
        {
            return _context.Shipmentagent.Any(e => e.Id == id);
        }
    }
}
