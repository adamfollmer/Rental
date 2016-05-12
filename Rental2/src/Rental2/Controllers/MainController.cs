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
        private ApplicationDbContext _context;

        public MainController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Main
        public IActionResult Index(int? id, int? tenantID, int? paymentID)
        {
            var viewModel = new PropertyIndexData();
            viewModel.Payments = _context.Payments
                .Include(i => i.Tenant)
                .Include(i => i.Bill)
                .OrderBy(i => i.BillId);

            if (id != null)
            {
                ViewBag.RentalID = id.Value;
                viewModel.Payments = viewModel.Rentals.Where(i => i.YearlyRentalId == id.Value).Single().YearlyRental.Payments;
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

            RentalUserConnection yearlyRental = _context.RentalUserConnections.Single(m => m.YearlyRentalId == id);
            if (yearlyRental == null)
            {
                return HttpNotFound();
            }

            return View(yearlyRental);
        }

        // GET: Main/Create
        public IActionResult Create()
        {
            ViewData["TenantID"] = new SelectList(_context.Tenants, "ID", "LastName");
            ViewData["PropertyID"] = new SelectList(_context.YearlyRentals, "ID", "Property");
            return View();
        }

        // POST: Main/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RentalUserConnection yearlyRental)
        {
            if (ModelState.IsValid)
            {
                _context.RentalUserConnections.Add(yearlyRental);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["TenantID"] = new SelectList(_context.Tenants, "ID", "Tenants", yearlyRental.Tenant);
            ViewData["PropertyID"] = new SelectList(_context.YearlyRentals, "ID", "Property", yearlyRental.YearlyRental.PropertyID);
            return View(yearlyRental);
        }

        // GET: Main/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            RentalUserConnection yearlyRental = _context.RentalUserConnections.Single(m => m.YearlyRentalId == id);
            if (yearlyRental == null)
            {
                return HttpNotFound();
            }
            ViewData["TenantID"] = new SelectList(_context.Tenants, "ID", "Tenants", yearlyRental.Tenant);
            ViewData["PropertyID"] = new SelectList(_context.YearlyRentals, "ID", "Property", yearlyRental.YearlyRental.PropertyID);
            return View(yearlyRental);
        }

        // POST: Main/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RentalUserConnection yearlyRental)
        {
            if (ModelState.IsValid)
            {
                _context.Update(yearlyRental);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["TenantID"] = new SelectList(_context.Tenants, "ID", "Tenants", yearlyRental.Tenant);
            ViewData["PropertyID"] = new SelectList(_context.YearlyRentals, "ID", "Property", yearlyRental.YearlyRental.PropertyID);
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

            RentalUserConnection yearlyRental = _context.RentalUserConnections.Single(m => m.YearlyRentalId == id);
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
            RentalUserConnection yearlyRental = _context.RentalUserConnections.Single(m => m.YearlyRentalId == id);
            _context.RentalUserConnections.Remove(yearlyRental);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
