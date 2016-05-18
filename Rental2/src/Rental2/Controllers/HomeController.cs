using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Rental2.Models;
using Rental2.ViewModels;
using Microsoft.Data.Entity;

namespace Rental2.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;

        public HomeController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Properties()
        {
            var properties = db.Properties;
            List<Property> available = new List<Property>();
            foreach (var item in properties.ToList())
            {
                if (!item.Occupied)
                {
                    available.Add(item);
                }

            }
            ViewData["MyProp"] = available;
            return View(available);
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
