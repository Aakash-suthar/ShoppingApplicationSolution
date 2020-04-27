using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipmentApi.model;

namespace ShipmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentagentsController : ControllerBase
    {
        private readonly shoppingdbContext _context;

        public ShipmentagentsController(shoppingdbContext context)
        {
            _context = context;
        }

        // GET: api/Shipmentagents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shipmentagent>>> GetShipmentagent()
        {
            return await _context.Shipmentagent.ToListAsync();
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
        public async Task<ActionResult<Shipmentagent>> PostShipmentagent(Shipmentagent shipmentagents)
        {
            _context.Shipmentagent.Add(shipmentagents);
            await _context.SaveChangesAsync();

            return Ok();
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

            return Ok();
        }

        private bool ShipmentagentExists(int id)
        {
            return _context.Shipmentagent.Any(e => e.Id == id);
        }
    }
}
