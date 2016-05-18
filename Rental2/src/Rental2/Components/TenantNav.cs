using Microsoft.AspNet.Mvc;
using Rental2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rental2.Components
{
    [ViewComponent]
    public class TenantNav : ViewComponent
    {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public TenantNav(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IViewComponentResult Invoke()
        {
            var navigationItems = LoadTenantNavItems();
            var viewModel = new ViewModel(navigationItems);
            return View(viewModel);
        }
        public async Task<ApplicationUser> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return user;
        }
        public Idefine LoadTenantNavItems()
        {
            var _user = GetCurrentUser().Result;
            if(_context.RentalUserConnections.FirstOrDefault(x => x.ApplicationUserId == _user.Id) != null)
            {
            RentalUserConnection connection = _context.RentalUserConnections.FirstOrDefault(x => x.ApplicationUserId == _user.Id);
            YearlyRental yearlyrental = _context.YearlyRentals.FirstOrDefault(x => x.ID == connection.YearlyRentalId);
            ICollection<Bill> bills = (ICollection<Bill>)_context.Bills.Where(x => x.YearlyRentalId == yearlyrental.ID).ToAsyncEnumerable();
            ICollection<Payment> payments = null;
            foreach (Bill _bill in bills)
            {
                payments = (ICollection<Payment>)_context.Payments.Where(x => x.BillId == _bill.ID).ToAsyncEnumerable();
            }
            var amountDue = CalculateBalance(bills, payments);
            var userData = new ItemViewModel()
            {
                UserProperty = (Property)_context.Properties.Where(x => x.ID == yearlyrental.PropertyID),
                CurrentUser = _user,
                BalanceRemaining = amountDue,
                CurrentBill = bills.ElementAt(0)
            };
                return userData;
            }
            else
            {
                var userData = new BareViewModel()
                {
                    CurrentUser = _user
                };
                return userData;
            }
        }
        public decimal CalculateBalance(ICollection<Bill> bills, ICollection<Payment> payments)
        {
            decimal TotalBill = 0;
            foreach (Bill bill in bills)
            {
                if (bill.DueDate.DayOfYear < DateTime.Now.DayOfYear)
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
            public BareViewModel BaseNavItems { get; }
            public ViewModel(Idefine navigationItems)
            {
                if(navigationItems.CurrentUser.RentalHistory.Count != 0)
                {
                NavigationItems = (ItemViewModel)navigationItems;
                }else
                {
                BaseNavItems = (BareViewModel)navigationItems;
                }
            }
        }
        public interface Idefine
            {
            ApplicationUser CurrentUser { get; set; }
        }
        public class ItemViewModel : Idefine
        {
            public Property UserProperty { get; set; }
            public ApplicationUser CurrentUser { get; set; }
            public Bill CurrentBill { get; set; }
            public decimal BalanceRemaining { get; set; }
        }
        public class BareViewModel : Idefine
        {
            public ApplicationUser CurrentUser { get; set; }
        }
    }
}