using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PaymentApi.Model;

namespace PaymentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  //  [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly shoppingdbContext _context;

        public PaymentsController(shoppingdbContext context)
        {
            _context = context;
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayment()
        {
            return await _context.Payment.ToListAsync();
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            var payment = await _context.Payment.FindAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            return payment;
        }

        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(Payment payments)
        {
            _context.Payment.Add(payments);
            await _context.SaveChangesAsync();
            return payments;
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Payment>> DeletePayment(int id)
        {
            var payment = await _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            _context.Payment.Remove(payment);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool PaymentExists(int id)
        {
            return _context.Payment.Any(e => e.Id == id);
        }
    }
}
