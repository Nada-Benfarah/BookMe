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
    public class LivresController : Controller
    {
        private readonly BookMeContext _context;

        public LivresController(BookMeContext context)
        {
            _context = context;
        }

        // GET: Livres
        public async Task<IActionResult> Index(int? themeId)
        {
            // Load all Themes for the sidebar.
            ViewBag.Themes = await _context.Themes.ToListAsync();

            // Prepare a query to load all Livres and their associated AuteurLivres, Auteurs, LivreThemes, and Themes.
            var query = _context.Livres
                .Include(l => l.AuteurLivres)
                .ThenInclude(al => al.Auteur)
                .Include(l => l.LivreThemes)
                .ThenInclude(lt => lt.Theme)
                .AsQueryable();  // Use AsQueryable to allow further modification of the query.

            // If a ThemeId was provided, filter the Livres by this ThemeId.
            if (themeId != null)
            {
                query = query.Where(l => l.LivreThemes.Any(lt => lt.ThemeId == themeId));
            }

            // Execute the query and return the result to the view.
            return View(await query.ToListAsync());
        }



        // GET: Livres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Livres == null)
            {
                return NotFound();
            }

            var livre = await _context.Livres
                .FirstOrDefaultAsync(m => m.LivreId == id);
            if (livre == null)
            {
                return NotFound();
            }

            return View(livre);
        }

        // GET: Livres/Create
        public IActionResult Create()
        {
            var auteurs = _context.Auteurs.ToList();

            var themes = _context.Themes.ToList();

            ViewBag.Auteurs = auteurs.Select(a =>
                new SelectListItem
                {
                    Text = a.Name,
                    Value = a.AuteurId.ToString(),
                });
            ViewBag.Themes = themes.Select(a =>
                new SelectListItem
                {
                    Text = a.Libelle,
                    Value = a.ThemeId.ToString(),
                });

            var model = new LivreCreateViewModel
            {
                Livre = new Livre(),
                SelectedAuteurIds = new List<int>(),
                SelectedThemesIds = new List<int>()
            };

            return View(model);
        }

        // POST: Livres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LivreCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var c = _context.Auteurs.ToList();
                ViewBag.Auteurs = c.Select(c =>
                    new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.AuteurId.ToString(),
                    });
                var d = _context.Themes.ToList();
                ViewBag.Themes = d.Select(d =>
                    new SelectListItem
                    {
                        Text = d.Libelle,
                        Value = d.ThemeId.ToString(),
                    });
                return View(model);
            }

            var livre = model.Livre;
            _context.Add(livre);
            await _context.SaveChangesAsync();

            foreach (var auteurId in model.SelectedAuteurIds)
            {
                var relation = new AuteurLivre();
                relation.LivreId = livre.LivreId;
                relation.AuteurId = auteurId;
                _context.Add(relation);
            }

            foreach (var themeId in model.SelectedThemesIds)
            {
                var relation = new LivreTheme();
                relation.LivreId = livre.LivreId;
                relation.ThemeId = themeId;
                _context.Add(relation);
            }
            await _context.SaveChangesAsync();

            TempData["success"] = "Book Created Successfully";
            return RedirectToAction(nameof(Index));
        }
        // GET: Livres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Livres == null)
            {
                return NotFound();
            }

            var livre = await _context.Livres
                .Include(l => l.AuteurLivres)
                .Include(l => l.LivreThemes)
                .FirstOrDefaultAsync(l => l.LivreId == id);
            if (livre == null)
            {
                return NotFound();
            }

            var model = new LivreEditViewModel
            {
                Livre = livre,
                SelectedAuteurIds = livre.AuteurLivres?.Select(al => al.AuteurId).ToList(),
                SelectedThemesIds = livre.LivreThemes?.Select(lt => lt.ThemeId).ToList()
            };
            ViewBag.Auteurs = _context.Auteurs.Select(a =>
        new SelectListItem
        {
            Text = a.Name,
            Value = a.AuteurId.ToString(),
        });

            ViewBag.Themes = _context.Themes.Select(t =>
                new SelectListItem
                {
                    Text = t.Libelle,
                    Value = t.ThemeId.ToString(),
                });


            return View(model);
        }

        // POST: Livres/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LivreEditViewModel model)
        {
            if (id != model.Livre.LivreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update the Livre and its relationships here.
                    var livre = await _context.Livres.Include(l => l.AuteurLivres).Include(l => l.LivreThemes).FirstOrDefaultAsync(l => l.LivreId == id);

                    // Updating the basic properties of the book.
                    livre.Name = model.Livre.Name;
                    livre.Language = model.Livre.Language;
                    livre.pagesNumbers = model.Livre.pagesNumbers;
                    livre.ImagePath = model.Livre.ImagePath;
                    livre.Description = model.Livre.Description;

                    // Handling the relationships updates. 
                    // This is a simplistic approach and you might need to adapt it based on your needs.
                    livre.AuteurLivres.Clear();
                    foreach (var auteurId in model.SelectedAuteurIds)
                    {
                        var relation = new AuteurLivre { LivreId = livre.LivreId, AuteurId = auteurId };
                        livre.AuteurLivres.Add(relation);
                    }

                    livre.LivreThemes.Clear();
                    foreach (var themeId in model.SelectedThemesIds)
                    {
                        var relation = new LivreTheme { LivreId = livre.LivreId, ThemeId = themeId };
                        livre.LivreThemes.Add(relation);
                    }

                    _context.Update(livre);
                    await _context.SaveChangesAsync();

                    TempData["success"] = "Book Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivreExists(model.Livre.LivreId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(model);
        }


        // GET: Livres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Livres == null)
            {
                return NotFound();
            }

            var livre = await _context.Livres
                .FirstOrDefaultAsync(m => m.LivreId == id);
            if (livre == null)
            {
                return NotFound();
            }

            return View(livre);
        }

        // POST: Livres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Livres == null)
            {
                return Problem("Entity set 'AppDbContext.Livres'  is null.");
            }
            var livre = await _context.Livres.FindAsync(id);
            if (livre != null)
            {
                _context.Livres.Remove(livre);
            }

            await _context.SaveChangesAsync();
            TempData["success"] = "Book Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool LivreExists(int id)
        {
            return (_context.Livres?.Any(e => e.LivreId == id)).GetValueOrDefault();
        }
    }
}
