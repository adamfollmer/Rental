using Microsoft.AspNet.Mvc;
using Rental2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental2.Components
{
    public class TenantNav : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var navigationItems = await LoadTenantNavItems();
            var viewModel = new ViewModel(navigationItems);
            return View(viewModel);
        }
        public class ViewModel
        {
            public IList<ItemViewModel> NavigationItems { get; }
            public ViewModel(IList<ItemViewModel> navigationItems)
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
            public decimal BalanceRemaining  { get; set; }

            public async Task<IList<ItemViewModel>> LoadTenantNaveItems()
            {

            }


        }
    }

}