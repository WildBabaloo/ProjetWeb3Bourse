using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetWeb3Bourse.Data;
using ProjetWeb3Bourse.Models;

namespace ProjetWeb3Bourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BourseAPIController : ControllerBase
    {
        private readonly BourseContext _context;

        public BourseAPIController(BourseContext context)
        {
            _context = context;
        }

        // GET: api/BourseAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bourse>>> GetBourse()
        {
          if (_context.Bourse == null)
          {
              return NotFound();
          }
            return await _context.Bourse.ToListAsync();
        }

        // GET: api/BourseAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bourse>> GetBourse(int id)
        {
          if (_context.Bourse == null)
          {
              return NotFound();
          }
            var bourse = await _context.Bourse.FindAsync(id);

            if (bourse == null)
            {
                return NotFound();
            }

            return bourse;
        }

        // PUT: api/BourseAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBourse(int id, Bourse bourse)
        {
            if (id != bourse.id)
            {
                return BadRequest();
            }

            _context.Entry(bourse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("PutMovie", id, bourse);
        }

        // POST: api/BourseAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bourse>> PostBourse(Bourse bourse)
        {
          if (_context.Bourse == null)
          {
              return Problem("Entity set 'ProjetWeb3BourseContext.Bourse'  is null.");
          }
            _context.Bourse.Add(bourse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBourse", new { id = bourse.id }, bourse);
        }

        // DELETE: api/BourseAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBourse(int id)
        {
            if (_context.Bourse == null)
            {
                return NotFound();
            }
            var bourse = await _context.Bourse.FindAsync(id);
            if (bourse == null)
            {
                return NotFound();
            }

            _context.Bourse.Remove(bourse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("DeleteMovie", id, bourse);
        }

        private bool BourseExists(int id)
        {
            return (_context.Bourse?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
