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
        private RentalContext db;

        public HomeController(RentalContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var yearly = db.YearlyRentals.Include(c => c.CurrentTenant).Include(c => c.Property);
            return View(yearly.ToList());
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
