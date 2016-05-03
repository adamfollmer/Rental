using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Rental2.Models;
using Rental2.ViewModels;

namespace Rental2.Controllers
{
    public class MainController : Controller
    {
        private RentalContext _context;

        public MainController(RentalContext context)
        {
            _context = context;    
        }

        // GET: Main
        public IActionResult Index(int? id, int? tenantID, int? paymentID)
        {
            var viewModel = new PropertyIndexData();
            viewModel.Rentals = _context.YearlyRentals
                .Include(i => i.MonthlyPayments)
                .Include(i => i.CurrentTenant)
                .Include(i => i.Property)
                .OrderBy(i => i.ID);

            if (id != null)
            {
                ViewBag.RentalID = id.Value;
                viewModel.Payments = viewModel.Rentals.Where(i => i.ID == id.Value).Single().MonthlyPayments;
            }

            return View(viewModel);
        }

        // GET: Main/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            YearlyRental yearlyRental = _context.YearlyRentals.Single(m => m.ID == id);
            if (yearlyRental == null)
            {
                return HttpNotFound();
            }

            return View(yearlyRental);
        }

        // GET: Main/Create
        public IActionResult Create()
        {
            ViewData["TenantID"] = new SelectList(_context.Tenants, "ID", "CurrentTenant");
            ViewData["PropertyID"] = new SelectList(_context.Properties, "ID", "Property");
            return View();
        }

        // POST: Main/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(YearlyRental yearlyRental)
        {
            if (ModelState.IsValid)
            {
                _context.YearlyRentals.Add(yearlyRental);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["TenantID"] = new SelectList(_context.Tenants, "ID", "CurrentTenant", yearlyRental.TenantID);
            ViewData["PropertyID"] = new SelectList(_context.Properties, "ID", "Property", yearlyRental.PropertyID);
            return View(yearlyRental);
        }

        // GET: Main/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            YearlyRental yearlyRental = _context.YearlyRentals.Single(m => m.ID == id);
            if (yearlyRental == null)
            {
                return HttpNotFound();
            }
            ViewData["TenantID"] = new SelectList(_context.Tenants, "ID", "CurrentTenant", yearlyRental.TenantID);
            ViewData["PropertyID"] = new SelectList(_context.Properties, "ID", "Property", yearlyRental.PropertyID);
            return View(yearlyRental);
        }

        // POST: Main/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(YearlyRental yearlyRental)
        {
            if (ModelState.IsValid)
            {
                _context.Update(yearlyRental);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["TenantID"] = new SelectList(_context.Tenants, "ID", "CurrentTenant", yearlyRental.TenantID);
            ViewData["PropertyID"] = new SelectList(_context.Properties, "ID", "Property", yearlyRental.PropertyID);
            return View(yearlyRental);
        }

        // GET: Main/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            YearlyRental yearlyRental = _context.YearlyRentals.Single(m => m.ID == id);
            if (yearlyRental == null)
            {
                return HttpNotFound();
            }

            return View(yearlyRental);
        }

        // POST: Main/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            YearlyRental yearlyRental = _context.YearlyRentals.Single(m => m.ID == id);
            _context.YearlyRentals.Remove(yearlyRental);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
