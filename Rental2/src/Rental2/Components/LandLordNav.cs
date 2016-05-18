using Microsoft.AspNet.Mvc;
using Rental2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental2.Components
{
    public class LandLordNav : ViewComponent
    {
        private ApplicationDbContext _context;
        public LandLordNav(ApplicationDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var navigationItems = LoadTenantNavItems();
            var viewModel = new ViewModel(navigationItems);
            viewModel.OutstandingBalances = CalculateBalance(navigationItems);
            viewModel.NumberOfUnrentedProperties = CalculateUnrentedProps(navigationItems);
            return View(viewModel);
        }
        public ICollection<ItemViewModel> LoadTenantNavItems()
        {
            var property = _context.Properties;
            List<ItemViewModel> userData = new List<ItemViewModel>();

            foreach (Property lot in property)
            {
                var lotDetail = new ItemViewModel()
                {
                    UserProperty = lot,
                    CurrentRental = _context.YearlyRentals.FirstOrDefault(x => x.PropertyID == lot.ID)
                };
                userData.Add(lotDetail);
            }
            return userData;
        }
        public decimal CalculateBalance(ICollection<ItemViewModel> RentalData)
        {
            decimal TotalBill = 0;
            foreach (ItemViewModel model in RentalData)
            {
                foreach (Bill bill in model.CurrentRental.Bills)
                    if (bill.DueDate.DayOfYear < DateTime.Now.DayOfYear)
                    {
                        TotalBill += bill.Amount;
                    }
                foreach (Payment payment in model.CurrentRental.Payments)
                {
                    TotalBill -= payment.PaymentAmount;
                }
            }
            return TotalBill;
        }
        public int CalculateUnrentedProps(ICollection<ItemViewModel> RentalData)
        {
            int TotalPropsUnrented = 0;
            foreach(ItemViewModel model in RentalData)
            {
                if(model.CurrentRental.EndDate > DateTime.Today)
                {
                    TotalPropsUnrented += 1;
                }
            }
            return TotalPropsUnrented;
        }
        public class ViewModel
        {
            public ICollection<ItemViewModel> NavigationItems { get; }
            public decimal OutstandingBalances { get; set; }
            public int NumberOfUnrentedProperties { get; set; }
            public ViewModel(ICollection<ItemViewModel> navigationItems)
            {
                NavigationItems = navigationItems;
            }

        }
        public class ItemViewModel
        {
            public virtual Property UserProperty { get; set; }
            public virtual YearlyRental CurrentRental { get; set; }
        }
    }
}