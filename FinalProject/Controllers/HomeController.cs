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
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
<<<<<<< HEAD
        }
        public IActionResult StartupTest()
<<<<<<< HEAD
        {            
            RootObject s = sd.getStart();                  
                return View(s);
=======
        {
            RootObject s = sd.getStart();
            return View(s);
>>>>>>> 85b87666efad595407cd8966ae7ed04a3d5518e0
        }
=======
        }        
>>>>>>> f1c630b5a2bfc41e8c67b5367600e61b91b27a45

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
<<<<<<< HEAD

    }

=======
    }
>>>>>>> 85b87666efad595407cd8966ae7ed04a3d5518e0
}
