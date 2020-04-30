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
    public class Shipmentagent3Controller : ControllerBase
    {
        private readonly NewShoppingDbContext _context;

        public Shipmentagent3Controller(NewShoppingDbContext context)
        {
            _context = context;
        }

        // GET: api/Shipmentagent3
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shipmentagent3>>> GetShipmentagent3()
        {
            return await _context.Shipmentagent3.ToListAsync();
        }

        // GET: api/Shipmentagent3/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shipmentagent3>> GetShipmentagent3(int id)
        {
            var shipmentagent3 = await _context.Shipmentagent3.FindAsync(id);

            if (shipmentagent3 == null)
            {
                return NotFound();
            }

            return shipmentagent3;
        }

        // PUT: api/Shipmentagent3/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut()]
        public IActionResult PutShipmentagent3(Shipmentagent3 shipmentagent3)
        {

            Shipmentagent3 s =  _context.Shipmentagent3.Find(shipmentagent3.Id);

            if (s != null)
            {
                s.DeliveryGuy = shipmentagent3.DeliveryGuy;
                s.Statuss = shipmentagent3.Statuss;
                s.Orderid = shipmentagent3.Orderid;

                 _context.SaveChanges();
            }
            else
            {
                return BadRequest();
            }
            return Ok();
        }

        // POST: api/Shipmentagent3
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Shipmentagent3>> PostShipmentagent3(Shipmentagent3 shipmentagent3)
        {
            _context.Shipmentagent3.Add(shipmentagent3);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Shipmentagent3/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Shipmentagent3>> DeleteShipmentagent3(int id)
        {
            var shipmentagent3 = await _context.Shipmentagent3.FindAsync(id);
            if (shipmentagent3 == null)
            {
                return NotFound();
            }

            _context.Shipmentagent3.Remove(shipmentagent3);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool Shipmentagent3Exists(int id)
        {
            return _context.Shipmentagent3.Any(e => e.Id == id);
        }
    }
}
