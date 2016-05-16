using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Rental2.Models;
using System.Collections.Generic;
using Rental2.ViewModels.RentalSetup;

namespace Rental2.Controllers
{
    public class YearlyRentalsController : Controller
    {
        private ApplicationDbContext _context;

        public YearlyRentalsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: YearlyRentals
        //fuson

        public IActionResult Index(string sortOrder, string searchString)
        {
            //ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            //ViewBag.DateSortParm = sortOrder == "tenant" ? "tenant_desc" : "tenant";
            //var rentalContext = from s in _context.YearlyRentals.
            //    Include(y => y.Tenants).
            //    Include(y => y.Property)
            //                    select s;
            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    rentalContext = rentalContext.Where(s => s.Tenants.ElementAt(0).Tenant.LastName.Contains(searchString)
            //    || s.Tenants.ElementAt(0).ApplicationUserId.ToString().Contains(searchString));
            //}
            //switch (sortOrder)
            //{
            //    case "id_desc":
            //        rentalContext = rentalContext.OrderByDescending(s => s.Tenants.ElementAt(0).ApplicationUserId);
            //        break;
            //    case "tenant":
            //        rentalContext = rentalContext.OrderBy(s => s.Tenants.ElementAt(0).Tenant.LastName);
            //        break;
            //    case "tenant_desc":
            //        rentalContext = rentalContext.OrderByDescending(s => s.Tenants.ElementAt(0).Tenant.LastName);
            //        break;
            //    default:
            //        rentalContext = rentalContext.OrderBy(s => s.Tenants.ElementAt(0).ApplicationUserId);
            //        break;
            //}
            var rentalContext = _context.YearlyRentals
                .Include(y => y.Tenants)
                .Include(y => y.Property);
            return View(rentalContext.ToList());
        }

        // GET: YearlyRentals/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            YearlyRental yearlyRental = _context.YearlyRentals
                .Include(y => y.Tenants)
                .Include(y => y.Property)
                .Include(y => y.Bills)
                .Include(y => y.Payments)
                .Single(m => m.ID == id);
            if (yearlyRental == null)
            {
                return HttpNotFound();
            }

            return View(yearlyRental);
        }

        // GET: YearlyRentals/Create
        public IActionResult Create()
        {
            ViewBag.TenantItems = GetTenantsListItems();
            ViewBag.PropertyItems = GetPropertiesListItems();
            return View();
        }

        // POST: YearlyRentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RentingConnectionData viewContextData)
        {           

            if (ModelState.IsValid)
            { 
                YearlyRental yearlyRental = new YearlyRental() { ID = viewContextData.ID, EndDate = viewContextData.EndDate, PropertyID = viewContextData.PropertyID, StartDate = viewContextData.StartDate };
                foreach(var i in viewContextData.TenantIds)
                {
                RentalUserConnection connection = new RentalUserConnection() { ApplicationUserId = i, YearlyRentalId = yearlyRental.ID };
                    _context.RentalUserConnections.Add(connection);
                }
                _context.YearlyRentals.Add(yearlyRental); 
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TenantItems = GetTenantsListItems();
            ViewBag.PropertyItems = GetPropertiesListItems();
            return View(viewContextData);
        }
        private IEnumerable<SelectListItem> GetTenantsListItems(int selected = -1)
        {
            var tmp = _context.Tenants.ToList();

            return tmp
                .OrderBy(tenant => tenant.Email)
                .Select(tenant => new SelectListItem
                {
                    Text = string.Format("{0}", tenant.UserName),
                    Value = tenant.Id,
                    Selected = tenant.Id == selected.ToString()
                });
        }

        private IEnumerable<SelectListItem> GetPropertiesListItems(int selected = -1)
        {
            var tmp = _context.Properties.ToList();

            return tmp
                .OrderBy(property => property.Address)
                .Select(property => new SelectListItem
                {
                    Text = string.Format("{0}", property.Address),
                    Value = property.ID.ToString(),
                    Selected = property.ID == selected
                });
        }

        // GET: YearlyRentals/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            RentalUserConnection yearlyRental = _context.RentalUserConnections
                .Single(m => m.YearlyRentalId == id);
            if (yearlyRental == null)
            {
                return HttpNotFound();
            }
            ViewBag.TenantItems = GetTenantsListItems();
            ViewBag.PropertyItems = GetPropertiesListItems();
            return View(yearlyRental);
        }

        // POST: YearlyRentals/Edit/5
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
            ViewData["TenantID"] = new SelectList(_context.Tenants, "ID", "Tenants", yearlyRental.ApplicationUserId);
            ViewData["PropertyID"] = new SelectList(_context.YearlyRentals, "ID", "Property", yearlyRental.YearlyRental.PropertyID);
            return View(yearlyRental);
        }

        // GET: YearlyRentals/Delete/5
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

        // POST: YearlyRentals/Delete/5
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