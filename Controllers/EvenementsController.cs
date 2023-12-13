using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetWeb3Bourse.Data;
using ProjetWeb3Bourse.Models;

namespace ProjetWeb3Bourse.Controllers {
    public class EvenementsController : Controller {
        private readonly BourseContext _context;

        public EvenementsController(BourseContext context) {
            _context = context;
        }

        // GET: Evenements
        //public async Task<IActionResult> Index()
        //{
        //    var bourseContext = _context.Evenement.Include(e => e.bourse);
        //    return View(await bourseContext.ToListAsync());
        //}

        public async Task<IActionResult> Index() {
            var query = from evenement in _context.Set<Evenement>()
                        join bourse in _context.Set<Bourse>()
                        on evenement.bourseId equals bourse.id
                        select new BourseEventViewModel { Bourse = bourse, Evenement = evenement };

            return View(await query.ToListAsync());
        }

        // GET: Evenements/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Evenement == null) {
                return NotFound();
            }

            var evenement = await _context.Evenement
                .Include(e => e.bourse)
                .FirstOrDefaultAsync(m => m.id == id);
            if (evenement == null) {
                return NotFound();
            }

            return View(evenement);
        }

        // GET: Evenements/Create/5
        public IActionResult Create(int id) {
            ViewData["bourseId"] = new SelectList(_context.Bourse, "id", "id", id);
            ViewData["eventId"] = id;
            return View();
        }

        // POST: Evenements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,bourseId,date,heure,valeur,variation")] Evenement evenement) {
            if (ModelState.IsValid) {
                _context.Add(evenement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["bourseId"] = new SelectList(_context.Bourse, "id", "id", evenement.bourseId);
            return View(evenement);
        }

        // GET: Evenements/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Evenement == null) {
                return NotFound();
            }

            var evenement = await _context.Evenement.FindAsync(id);
            if (evenement == null) {
                return NotFound();
            }
            ViewData["bourseId"] = new SelectList(_context.Bourse, "id", "id", evenement.bourseId);
            return View(evenement);
        }

        // POST: Evenements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,bourseId,date,heure,valeur,variation")] Evenement evenement) {
            if (id != evenement.id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(evenement);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!EvenementExists(evenement.id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["bourseId"] = new SelectList(_context.Bourse, "id", "id", evenement.bourseId);
            return View(evenement);
        }

        // GET: Evenements/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Evenement == null) {
                return NotFound();
            }

            var evenement = await _context.Evenement
                .Include(e => e.bourse)
                .FirstOrDefaultAsync(m => m.id == id);
            if (evenement == null) {
                return NotFound();
            }

            return View(evenement);
        }

        // POST: Evenements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Evenement == null) {
                return Problem("Entity set 'BourseContext.Evenement'  is null.");
            }
            var evenement = await _context.Evenement.FindAsync(id);
            if (evenement != null) {
                _context.Evenement.Remove(evenement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvenementExists(int id) {
            return (_context.Evenement?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}