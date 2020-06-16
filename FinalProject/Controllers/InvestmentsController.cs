using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult InvestmentsIndex()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserPreferences thisUsersPreferences = _context.UserPreferences.FirstOrDefault();
            return View(thisUsersPreferences);
        }
        public IActionResult Search(string companyName, string country, string city, string theme, string technologyAreas, string alignment, int? rating)
        {
            IEnumerable<Record> s = sd.getStart().records;
            Startups.RateStartups(s);
            IEnumerable<Record> found = s.Where(x =>
            (string.IsNullOrWhiteSpace(companyName) || x.startups.CompanyName.ToLower() == companyName.ToLower())
            && (string.IsNullOrWhiteSpace(country) || x.startups.Country.ToLower() == country.ToLower())
            && (string.IsNullOrWhiteSpace(city) || x.startups.City == city)
            && (string.IsNullOrWhiteSpace(theme) || x.startups.Themes != null && x.startups.Themes.Contains(theme))
            && (string.IsNullOrWhiteSpace(technologyAreas) || x.startups.TechnologyAreas != null && x.startups.TechnologyAreas.Contains(technologyAreas))
            && (string.IsNullOrWhiteSpace(alignment) || x.startups.Alignment != null && x.startups.Alignment.Contains(alignment))
            && (rating == null || x.startups.Rating >= rating));
            return View(found);
        }
        public IActionResult UserSurvey()
        {
            return View();
        }
        public IActionResult AddUserPreferences(string country, string city, string theme, string technologyAreas, string alignment, int? rating)
        {
            UserPreferences userPreferences = new UserPreferences();
            if (ModelState.IsValid)
            {
                userPreferences.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                userPreferences.Country = country;
                userPreferences.City = city;
                userPreferences.Theme = theme;
                userPreferences.TechArea = technologyAreas;
                userPreferences.Alignment = alignment;
                userPreferences.Rank = rating;
                _context.UserPreferences.Add(userPreferences);
                _context.SaveChanges();
                return RedirectToAction("InvestmentsIndex", userPreferences);
            }
            else
            {
                return RedirectToAction("InvestmentsIndex");
            }
        }
        public IActionResult RemoveUserPreferences(int id)
        {
            UserPreferences found = _context.UserPreferences.Find(id);
            if (found != null)
            {
                _context.UserPreferences.Remove(found);
                _context.SaveChanges();
            }
            return RedirectToAction("InvestmentsIndex");
        }
        public IActionResult UpdateUserPreferences(int id)
        {
            UserPreferences found = _context.UserPreferences.Find(id);            
            return View(found);
        }
        public IActionResult ChangeUserPreferences(int id, string country, string city, string theme, string technologyAreas, string alignment, int? rating)
        {
            UserPreferences found = _context.UserPreferences.Find(id);
            if (found != null)
            {
                found.Country = country;
                found.City = city;
                found.Theme = theme;
                found.TechArea = technologyAreas;
                found.Alignment = alignment;
                found.Rank = rating;
                _context.Entry(found).State = EntityState.Modified;
                _context.Update(found);
                _context.SaveChanges();
            }
            return RedirectToAction("InvestmentsIndex", found);
        }

        public IActionResult AddToFavorite(string name)
        {
            Favorite favorite = new Favorite
            {
                StartupName = name,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value
            };
            if (_context.Favorite.Where(x => (x.StartupName == name) && (x.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value)).ToList().Count > 0)
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

        public IActionResult Individual(string id)
        {
            Record r = sd.GetRecord(id);
            Startups.RateIndividual(r);
            return View(r);
        }
    }
}