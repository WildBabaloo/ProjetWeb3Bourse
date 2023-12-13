using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ProjetWeb3Bourse.Data;
using ProjetWeb3Bourse.Hubs;
using ProjetWeb3Bourse.Models;

namespace ProjetWeb3Bourse.Controllers
{
    public class BoursesController : Controller
    {
        private readonly BourseContext _context;

        private readonly IHubContext<BourseHub> _hubContext;

        public BoursesController(BourseContext context, IHubContext<BourseHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: Bourses
        public async Task<IActionResult> Index()
        {
            return _context.Bourse != null ? 
            View(await _context.Bourse.ToListAsync()) :
            Problem("Entity set 'ProjetWeb3BourseContext.Bourse'  is null.");
        }

        // GET: Bourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bourse == null)
            {
                return NotFound();
            }

            var bourse = await _context.Bourse
                .Include(b => b.Evenements)
                .FirstOrDefaultAsync(m => m.id == id);
            if (bourse == null)
            {
                return NotFound();
            }

            return View(bourse);
        }

        // GET: Bourses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bourses/Create/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nom,valeur,variation")] Bourse bourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bourse);
                await _context.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("NewBourse");
                return RedirectToAction(nameof(Index));
            }
            return View(bourse);
        }

        // GET: Bourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bourse == null)
            {
                return NotFound();
            }

            var bourse = await _context.Bourse.FindAsync(id);
            if (bourse == null)
            {
                return NotFound();
            }
            return View(bourse);
        }

        // POST: Bourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nom,valeur,variation")] Bourse bourse)
        {
            if (id != bourse.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BourseExists(bourse.id))
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
            return View(bourse);
        }

        // GET: Bourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bourse == null)
            {
                return NotFound();
            }

            var bourse = await _context.Bourse
                .FirstOrDefaultAsync(m => m.id == id);
            if (bourse == null)
            {
                return NotFound();
            }

            return View(bourse);
        }

        // POST: Bourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bourse == null)
            {
                return Problem("Entity set 'ProjetWeb3BourseContext.Bourse'  is null.");
            }
            var bourse = await _context.Bourse.FindAsync(id);
            if (bourse != null)
            {
                _context.Bourse.Remove(bourse);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BourseExists(int id)
        {
          return (_context.Bourse?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
