using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FinalProject.Models;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly SeamlessDAL RD = new SeamlessDAL();
        private readonly SeamlessDAL sd;
        public HomeController(IConfiguration configuration)
        {
            sd = new SeamlessDAL(configuration);
        }
        public IActionResult Index()
        {
            ViewBag.s = sd.GetAPIString();
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult StartupTest()
        {
            //string output = RD.GetAPIString("aww");
            //ViewBag.test = output;
            RootObject s = sd.getStart();
            
                //s.records[i].fields.Score = int.Parse(s.records[i].fields.Team) + int.Parse(s.records[i].fields.Uniqueness);
                //if (s.records[i].fields.Score == 10)
                //{
                //    s.records[i].fields.Rank = "Very Strong";
                //}
                //else if (s.records[i].fields.Score == 9)
                //{
                //    s.records[i].fields.Rank = "Strong";
                //}
                //else if (s.records[i].fields.Score == 8)
                //{
                //    s.records[i].fields.Rank = "Good";
                //}
                //else if (s.records[i].fields.Score == 7)
                //{
                //    s.records[i].fields.Rank = "Average";
                //}
                //else
                //{
                //    s.records[i].fields.Rank = "Weak";
                //}            
                return View(s);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
