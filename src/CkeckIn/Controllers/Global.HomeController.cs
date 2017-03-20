using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CkeckIn.Data;
using Microsoft.AspNetCore.Mvc;

namespace CkeckIn.Controllers
{    
    /// <summary>
    /// Реализует функции гостевого раздела сайта
    /// </summary>
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            using (var client = new ApplicationDbContext())
            {
                client.Database.EnsureCreated();
            }
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
