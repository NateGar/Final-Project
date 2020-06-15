using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FinalProject.Controllers
{
    public class InvestmentsController : Controller
    {
        private readonly SeamlessDAL sd;
        public InvestmentsController(IConfiguration configuration)
        {
            sd = new SeamlessDAL(configuration);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Search(string companyName, string country, string city, string theme, string technologyAreas, string alignment)
        {
            IEnumerable<Record> s = sd.getStart().records;
            IEnumerable<Record> found = s.Where(x =>
            (string.IsNullOrWhiteSpace(companyName) || x.startups.CompanyName.ToLower() == companyName.ToLower())
            && (string.IsNullOrWhiteSpace(country) || x.startups.Country.ToLower() == country.ToLower())
            && (string.IsNullOrWhiteSpace(city) || x.startups.City == city)
            && (string.IsNullOrWhiteSpace(theme) || x.startups.Themes == theme)
            && (string.IsNullOrWhiteSpace(technologyAreas) || x.startups.TechnologyAreas == technologyAreas)
            && (string.IsNullOrWhiteSpace(alignment) || x.startups.Alignment == alignment));
             
            return View(found);
        }
    }
}