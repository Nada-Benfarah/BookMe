using BookMe.Data;
using BookMe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookMe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookMeContext _context;

        public HomeController(ILogger<HomeController> logger, BookMeContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Index(string AuteurName, string ThemeLibelle, string LivreName)
        {
            var viewModel = new LivreThemeViewModel();

            // Filtrer les livres en fonction des paramètres de recherche
            IQueryable<Livre> livres = _context.Livres
              .Include(l => l.AuteurLivres)
              .ThenInclude(al => al.Auteur)
              .Include(l => l.LivreThemes)
              .ThenInclude(lt => lt.Theme);

            if (!string.IsNullOrEmpty(AuteurName))
            {
                livres = livres.Where(l => l.AuteurLivres.Any(al => al.Auteur.Name.Contains(AuteurName)));
            }

            if (!string.IsNullOrEmpty(ThemeLibelle))
            {
                livres = livres.Where(l => l.LivreThemes.Any(lt => lt.Theme.Libelle.Contains(ThemeLibelle)));
            }

            if (!string.IsNullOrEmpty(LivreName))
            {
                livres = livres.Where(l => l.Name.Contains(LivreName));
            }

            viewModel.Livres = await livres.ToListAsync();
            viewModel.Themes = await _context.Themes.ToListAsync();

            return View(viewModel);
        }





        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Themes()
        {
            var themes = await _context.Themes.ToListAsync();
            return View(themes);
        }
    }
}