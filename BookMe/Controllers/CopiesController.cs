using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookMe.Models;
using BookMe.Data;

namespace BookMe.Controllers
{
    public class CopiesController : Controller
    {
        private readonly BookMeContext _context;

        public CopiesController(BookMeContext context)
        {
            _context = context;
        }

        // GET: Copies
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Copies.Include(c => c.Livre);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Copies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Copies == null)
            {
                return NotFound();
            }

            var copie = await _context.Copies
                .Include(c => c.Livre)
                .FirstOrDefaultAsync(m => m.CopieId == id);
            if (copie == null)
            {
                return NotFound();
            }

            return View(copie);
        }

        // GET: Copies/Create
        public IActionResult Create()
        {
            ViewData["LivreId"] = new SelectList(_context.Livres, "LivreId", "LivreId");
            return View();
        }

        // POST: Copies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CopieId,Etat,Edition,LivreId")] Copie copie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(copie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LivreId"] = new SelectList(_context.Livres, "LivreId", "LivreId", copie.LivreId);
            return View(copie);
        }

        // GET: Copies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Copies == null)
            {
                return NotFound();
            }

            var copie = await _context.Copies.FindAsync(id);
            if (copie == null)
            {
                return NotFound();
            }
            ViewData["LivreId"] = new SelectList(_context.Livres, "LivreId", "LivreId", copie.LivreId);
            return View(copie);
        }

        // POST: Copies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CopieId,Etat,Edition,LivreId")] Copie copie)
        {
            if (id != copie.CopieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(copie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CopieExists(copie.CopieId))
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
            ViewData["LivreId"] = new SelectList(_context.Livres, "LivreId", "LivreId", copie.LivreId);
            return View(copie);
        }

        // GET: Copies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Copies == null)
            {
                return NotFound();
            }

            var copie = await _context.Copies
                .Include(c => c.Livre)
                .FirstOrDefaultAsync(m => m.CopieId == id);
            if (copie == null)
            {
                return NotFound();
            }

            return View(copie);
        }

        // POST: Copies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Copies == null)
            {
                return Problem("Entity set 'AppDbContext.Copies'  is null.");
            }
            var copie = await _context.Copies.FindAsync(id);
            if (copie != null)
            {
                _context.Copies.Remove(copie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CopieExists(int id)
        {
          return (_context.Copies?.Any(e => e.CopieId == id)).GetValueOrDefault();
        }
    }
}
