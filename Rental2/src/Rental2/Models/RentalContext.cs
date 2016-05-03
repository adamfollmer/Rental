using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace Rental2.Models
{
    public class RentalContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }
        public DbSet<YearlyRental> YearlyRentals { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Document> Documents { get; set; }
    }
}
