//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.Data.Entity;

//namespace Rental2.Models
//{
//    public class RentalContext : DbContext
//    {
//        public DbSet<Payment> Payments { get; set; }
//        public DbSet<YearlyRental> YearlyRentals { get; set; }
//        public DbSet<Property> Properties { get; set; }
//        public DbSet<ApplicationUser> Tenants { get; set; }
//        public DbSet<Document> Documents { get; set; }
//        public DbSet<Bill> Bills { get; set; }
//        public DbSet<RentalUserConnection> RentalUserConnections { get; set; }
//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {

//            modelBuilder.Entity<RentalUserConnection>()
//                .HasKey(t => new { t.ApplicationUserId, t.YearlyRentalId });

//            modelBuilder.Entity<RentalUserConnection>()
//                .HasOne(pt => pt.YearlyRental)
//                .WithMany(t => t.Tenants)
//                .HasForeignKey(pt => pt.YearlyRentalId);

//            modelBuilder.Entity<RentalUserConnection>()
//                .HasOne(pt => pt.Tenant)
//                .WithMany(t => t.RentalHistory)
//                .HasForeignKey(pt => pt.ApplicationUserId);
//        }
//    }
//}
