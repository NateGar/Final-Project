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
            RootObject s = sd.getStart();                  
                return View(s);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }

}
