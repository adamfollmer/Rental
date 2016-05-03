using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rental2.Models;

namespace Rental2.ViewModels
{
    public class PropertyIndexData
    {
        public IEnumerable<YearlyRental> Rentals { get; set; }
        public IEnumerable<Payment> Payments { get; set; }
        public IEnumerable<Property> Properties { get; set; }
        public IEnumerable<Tenant> Tenants { get; set; }
        
    }
}
