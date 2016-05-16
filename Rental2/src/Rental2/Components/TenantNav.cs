using Microsoft.AspNet.Mvc;
using Rental2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Rental2.Components
{
    public class TenantNav : ViewComponent
    {
        //public async task<iviewcomponentresult> invokeasync()
        //{
        //    var navigationitems = await loadtenantnavitems();
        //    var viewmodel = new viewmodel(navigationitems);
        //    return view(viewmodel);

        //}
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

            //public async Task<IList<ItemViewModel>> LoadTenantNaveItems()
            //{

            //}


        }
    }

}