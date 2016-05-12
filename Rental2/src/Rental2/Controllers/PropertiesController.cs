using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Rental2.Models;
using Rental2.ViewModels;
using System.Collections.Generic;
using Microsoft.AspNet.Authorization;

namespace Rental2.Controllers
{
    public class PropertiesController : Controller
    {
        private ApplicationDbContext _context;

        public PropertiesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Properties
        //fuson
        [Authorize(Roles = "Tenant")]
        public IActionResult Index()
        {
            var properties = _context.Properties.Include(p => p.PastRentals).ToList();
            return View(properties);
        }

        // GET: Properties/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Property property = _context.Properties
                .Include(m => m.PastRentals)
                .Single(m => m.ID == id);
            if (property == null)
            {
                return HttpNotFound();
            }

            return View(property);
        }

        // GET: Properties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Properties/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Property property)
        {
            if (ModelState.IsValid)
            {
                _context.Properties.Add(property);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(property);
        }

        // GET: Properties/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Property property = _context.Properties.Single(m => m.ID == id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: Properties/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Property property)
        {
            if (ModelState.IsValid)
            {
                _context.Update(property);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(property);
        }

        // GET: Properties/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Property property = _context.Properties.Single(m => m.ID == id);
            if (property == null)
            {
                return HttpNotFound();
            }

            return View(property);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Property property = _context.Properties.Single(m => m.ID == id);
            _context.Properties.Remove(property);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
