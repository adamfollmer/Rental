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
            ICollection<Property> property = (ICollection<Property>)from t in _context.Properties.AsEnumerable()
                                                                    select t;
            ICollection<ItemViewModel> userData = null;

            foreach (Property lot in property)
            {
                var lotDetail = new ItemViewModel()
                {
                    PropertyID = lot.ID,
                    UserProperty = lot,
                    CurrentRental = _context.YearlyRentals.FirstOrDefault(x => x.PropertyID == lot.ID)
                };
                lotDetail.YearlyRentalId = lotDetail.CurrentRental.ID;
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
            public int PropertyID { get; set; }
            public virtual Property UserProperty { get; set; }
            public int YearlyRentalId { get; set; }
            public virtual YearlyRental CurrentRental { get; set; }
        }
    }
}