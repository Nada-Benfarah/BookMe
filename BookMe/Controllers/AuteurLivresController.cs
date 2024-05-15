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
    public class AuteurLivresController : Controller
    {
        private readonly BookMeContext _context;

        public AuteurLivresController(BookMeContext context)
        {
            _context = context;
        }

        // GET: AuteurLivres
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AuteurLivres.Include(a => a.Auteur).Include(a => a.Livre);
            return View(await appDbContext.ToListAsync());
        }

        // GET: AuteurLivres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AuteurLivres == null)
            {
                return NotFound();
            }

            var auteurLivre = await _context.AuteurLivres
                .Include(a => a.Auteur)
                .Include(a => a.Livre)
                .FirstOrDefaultAsync(m => m.AuteurId == id);
            if (auteurLivre == null)
            {
                return NotFound();
            }

            return View(auteurLivre);
        }

        // GET: AuteurLivres/Create
        public IActionResult Create()
        {
            ViewData["AuteurId"] = new SelectList(_context.Auteurs, "AuteurId", "AuteurId");
            ViewData["LivreId"] = new SelectList(_context.Livres, "LivreId", "LivreId");
            return View();
        }

        // POST: AuteurLivres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuteurId,LivreId")] AuteurLivre auteurLivre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(auteurLivre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuteurId"] = new SelectList(_context.Auteurs, "AuteurId", "AuteurId", auteurLivre.AuteurId);
            ViewData["LivreId"] = new SelectList(_context.Livres, "LivreId", "LivreId", auteurLivre.LivreId);
            return View(auteurLivre);
        }

        // GET: AuteurLivres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AuteurLivres == null)
            {
                return NotFound();
            }

            var auteurLivre = await _context.AuteurLivres.FindAsync(id);
            if (auteurLivre == null)
            {
                return NotFound();
            }
            ViewData["AuteurId"] = new SelectList(_context.Auteurs, "AuteurId", "AuteurId", auteurLivre.AuteurId);
            ViewData["LivreId"] = new SelectList(_context.Livres, "LivreId", "LivreId", auteurLivre.LivreId);
            return View(auteurLivre);
        }

        // POST: AuteurLivres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuteurId,LivreId")] AuteurLivre auteurLivre)
        {
            if (id != auteurLivre.AuteurId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auteurLivre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuteurLivreExists(auteurLivre.AuteurId))
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
            ViewData["AuteurId"] = new SelectList(_context.Auteurs, "AuteurId", "AuteurId", auteurLivre.AuteurId);
            ViewData["LivreId"] = new SelectList(_context.Livres, "LivreId", "LivreId", auteurLivre.LivreId);
            return View(auteurLivre);
        }

        // GET: AuteurLivres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AuteurLivres == null)
            {
                return NotFound();
            }

            var auteurLivre = await _context.AuteurLivres
                .Include(a => a.Auteur)
                .Include(a => a.Livre)
                .FirstOrDefaultAsync(m => m.AuteurId == id);
            if (auteurLivre == null)
            {
                return NotFound();
            }

            return View(auteurLivre);
        }

        // POST: AuteurLivres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AuteurLivres == null)
            {
                return Problem("Entity set 'AppDbContext.AuteurLivres'  is null.");
            }
            var auteurLivre = await _context.AuteurLivres.FindAsync(id);
            if (auteurLivre != null)
            {
                _context.AuteurLivres.Remove(auteurLivre);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuteurLivreExists(int id)
        {
          return (_context.AuteurLivres?.Any(e => e.AuteurId == id)).GetValueOrDefault();
        }
    }
}
