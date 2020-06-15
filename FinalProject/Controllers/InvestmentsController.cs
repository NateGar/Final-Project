using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FinalProject.Controllers
{
    public class InvestmentsController : Controller
    {
        private readonly SeamlessDAL sd;
        private readonly InvestmentsDbContext _context;
        public InvestmentsController(IConfiguration configuration, InvestmentsDbContext context)
        {
            sd = new SeamlessDAL(configuration);
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Search(string companyName, string country, string city, string theme, string technologyAreas, string alignment, int? rating)
        {
            IEnumerable<Record> s = sd.getStart().records;
            Startups.RateStartups(s);
            IEnumerable<Record> found = s.Where(x =>
            (string.IsNullOrWhiteSpace(companyName) || x.startups.CompanyName.ToLower() == companyName.ToLower())
            && (string.IsNullOrWhiteSpace(country) || x.startups.Country.ToLower() == country.ToLower())
            && (string.IsNullOrWhiteSpace(city) || x.startups.City == city)
            && (string.IsNullOrWhiteSpace(theme) || x.startups.Themes == theme)
            && (string.IsNullOrWhiteSpace(technologyAreas) || x.startups.TechnologyAreas == technologyAreas)
            && (string.IsNullOrWhiteSpace(alignment) || x.startups.Alignment == alignment)
            && (rating == null || x.startups.Rating >= rating));
            return View(found);
        }


        public IActionResult AddToFavorite(string id)
        {
            Favorite favorite = new Favorite
            {
                StartupId = id,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value
            };
            if (_context.Favorite.Where(x => (x.StartupId == id) && (x.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value)).ToList().Count > 0)
            {
                return RedirectToAction("Favorites");
            }
            if (ModelState.IsValid)
            {
                _context.Favorite.Add(favorite);
                _context.SaveChanges();
            }
            return RedirectToAction("Favorites");
        }

        public IActionResult Favorites()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var thisUsersFavorites = _context.Favorite.Where(x => x.UserId == id).ToList();
            return View(thisUsersFavorites);
        }

        public IActionResult RemoveFavorite(int id)
        {
            Favorite found = _context.Favorite.Find(id);
            if (found != null)
            {
                _context.Favorite.Remove(found);
                _context.SaveChanges();
            }
            return RedirectToAction("Favorites", new { id = found.Id });
        }
    }
}