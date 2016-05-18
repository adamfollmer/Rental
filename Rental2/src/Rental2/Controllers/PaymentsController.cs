using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Rental2.Models;
using System.Collections.Generic;
using Braintree;
using Rental2.Services;

namespace Rental2.Controllers
{
    public class PaymentsController : Controller
    {
        private ApplicationDbContext _context;
        public IGateway config = new PaymentGateway();
        public static readonly TransactionStatus[] transactionSuccessStatuses = {
                                                                                    TransactionStatus.AUTHORIZED,
                                                                                    TransactionStatus.AUTHORIZING,
                                                                                    TransactionStatus.SETTLED,
                                                                                    TransactionStatus.SETTLING,
                                                                                    TransactionStatus.SETTLEMENT_CONFIRMED,
                                                                                    TransactionStatus.SETTLEMENT_PENDING,
                                                                                    TransactionStatus.SUBMITTED_FOR_SETTLEMENT
                                                                                };


        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Payments
        public IActionResult Index()
        {
            ViewBag.items = _context.Tenants.ToList();
            var rentalContext = _context.Payments
                .Include(p => p.Tenant)
                .Include(p => p.Bill);
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
        //fuson
        private IEnumerable<SelectListItem> GetYearlyRentalItems (int selected = -1)
        {
            var tmp = _context.YearlyRentals
                .Include(m => m.EndDate)
                .ToList();

            return tmp
                .OrderBy(rental => rental.Tenants.ElementAt(0).Tenant.LastName)
                .Select(yearlyRentals => new SelectListItem
                {
                    Text = string.Format("{0}: {1}, {2}", yearlyRentals.ID, yearlyRentals.ID, yearlyRentals.EndDate),
                    Value = yearlyRentals.ID.ToString(),
                    Selected = yearlyRentals.ID == selected
                });
        }
        public ActionResult RentPayment()
        {
            var gateway = config.GetGateway();
            var clientToken = gateway.ClientToken.generate();
            ViewBag.ClientToken = clientToken;
            return View();
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
            ViewData["TenantID"] = new SelectList(_context.Tenants, "ID", "Tenant", payment.Bill.YearlyRentalId);
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
            ViewData["TenantID"] = new SelectList(_context.Tenants, "ID", "Tenant", payment.Bill.YearlyRentalId);
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
