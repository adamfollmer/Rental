using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Rental2.Models;
using System.Collections.Generic;

namespace Rental2.Controllers
{
    public class PaymentsController : Controller
    {
        private RentalContext _context;

        public PaymentsController(RentalContext context)
        {
            _context = context;    
        }

        // GET: Payments
        public IActionResult Index()
        {
            ViewBag.items = _context.Tenants.ToList();
            var rentalContext = _context.Payments.Include(p => p.YearlyRental);
            return View(rentalContext.ToList());
        }

        // GET: Payments/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Payment payment = _context.Payments.Single(m => m.ID == id);

            if (payment == null)
            {
                return HttpNotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            //ViewData["TenantID"] = new SelectList(_context.Tenants, "ID", "Tenant");
            ViewBag.Items = GetYearlyRentalItems();
            return View();
        }

        private IEnumerable<SelectListItem> GetYearlyRentalItems (int selected = -1)
        {
            var tmp = _context.YearlyRentals
                .Include(m => m.CurrentTenant)
                .ToList();

            return tmp
                .OrderBy(rental => rental.TenantID)
                .Select(yearlyRentals => new SelectListItem
                {
                    Text = string.Format("{0}: {1}", yearlyRentals.ID, yearlyRentals.CurrentTenant.Name),
                    Value = yearlyRentals.ID.ToString(),
                    Selected = yearlyRentals.ID == selected
                });
        }

        // POST: Payments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Payments.Add(payment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewData["TenantID"] = new SelectList(_context.Tenants, "ID", "Tenant", payment.TenantID);
            ViewBag.Items = GetYearlyRentalItems();
            return View(payment);
        }

        // GET: Payments/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Payment payment = _context.Payments.Single(m => m.ID == id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewData["TenantID"] = new SelectList(_context.Tenants, "ID", "Tenant", payment.YearlyRentalID);
            return View(payment);
        }

        // POST: Payments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Update(payment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["TenantID"] = new SelectList(_context.Tenants, "ID", "Tenant", payment.YearlyRentalID);
            return View(payment);
        }

        // GET: Payments/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Payment payment = _context.Payments.Single(m => m.ID == id);
            if (payment == null)
            {
                return HttpNotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Payment payment = _context.Payments.Single(m => m.ID == id);
            _context.Payments.Remove(payment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
