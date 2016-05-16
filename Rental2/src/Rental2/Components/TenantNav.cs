using Microsoft.AspNet.Mvc;
using Rental2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Collections;

namespace Rental2.Components
{
    public class TenantNav : ViewComponent
    {
        private ApplicationDbContext _context;
        private ApplicationUser _user;
        public TenantNav(ApplicationDbContext context)
        {
            _context = context;
            _user = (ApplicationUser)HttpContext.User.Identity;
        }
        public  IViewComponentResult Invoke()
        {
            var navigationItems =  LoadTenantNavItems();
            var viewModel = new ViewModel(navigationItems);
            return View(viewModel);
        }
        public ItemViewModel LoadTenantNavItems()
        {
            RentalUserConnection connection = _context.RentalUserConnections.FirstOrDefault(x => x.ApplicationUserId == _user.Id);
            YearlyRental yearlyrental = _context.YearlyRentals.FirstOrDefault(x => x.ID == connection.YearlyRentalId);
            ICollection<Bill> bills = (ICollection<Bill>)from t in _context.Bills where t.YearlyRentalId == yearlyrental.ID select t;
            ICollection<Payment> payments = null;
            foreach (Bill _bill in bills)
            {
                payments = (ICollection<Payment>)from t in _context.Payments where t.BillId == _bill.ID select t;
            }
            var amountDue = CalculateBalance(bills, payments);
            Property property = _context.Properties.FirstOrDefault(x => x.ID == yearlyrental.PropertyID);
            var userData = new ItemViewModel()
            {
                PropertyID = property.ID,
                UserProperty = property,
                UserId = _user.Id,
                User = _user,
                BalanceRemaining = amountDue,
                BillId = bills.ElementAt(0).ID,
                CurrentBill = bills.ElementAt(0)
            };
            return userData;
        }
        public decimal CalculateBalance(ICollection<Bill> bills, ICollection<Payment> payments)
        {
            decimal TotalBill = 0;
            foreach (Bill bill in bills)
            {
                if (bill.DueDate.DayOfYear > DateTime.Now.DayOfYear)
                {
                    TotalBill += bill.Amount;
                }
            }
            foreach (Payment payment in payments)
            {
                TotalBill -= payment.PaymentAmount;
            }
            return TotalBill;
        }
        public class ViewModel
        {
            public ItemViewModel NavigationItems { get; }
            public ViewModel(ItemViewModel navigationItems)
            {
                NavigationItems = navigationItems;
            }

        }
        public class ItemViewModel
        {
            public int PropertyID { get; set; }
            public Property UserProperty { get; set; }
            public string UserId { get; set; }
            public ApplicationUser User { get; set; }
            public int BillId { get; set; }
            public Bill CurrentBill { get; set; }
            public decimal BalanceRemaining { get; set; }
        }
    }

}