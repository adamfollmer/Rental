using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental2.Models
{
    public static class DataSeeder
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            var db = app.ApplicationServices.GetService<ApplicationDbContext>();
            db.Bills.AddRange(
                new Bill() { ID = 1, Amount = 500, Description = "January Rent", DueDate = new DateTime(2016, 01, 01), YearlyRentalId = 1 },
                new Bill() { ID = 2, Amount = 3000, Description = "January Rent", DueDate = new DateTime(2016, 01, 01), YearlyRentalId = 2 },
                new Bill() { ID = 3, Amount = 550, Description = "Feb Rent", DueDate = new DateTime(2016, 02, 01), YearlyRentalId = 1 },
                new Bill() { ID = 4, Amount = 500, Description = "March Rent", DueDate = new DateTime(2016, 03, 01), YearlyRentalId = 1 }
                );
            db.YearlyRentals.AddRange(
                new YearlyRental() { ID = 1, EndDate = new DateTime(2016, 12, 01), StartDate = new DateTime(2016, 01, 01), PropertyID = 1 },
                new YearlyRental() { ID = 2, EndDate = new DateTime(2016, 12, 01), StartDate = new DateTime(2016, 01, 01), PropertyID = 2 }
                );
            db.Properties.AddRange(
                new Property() { ID = 1, AcceptsCats = false, AcceptsDogs = true, Address = "1724 N Pulaski St Milwaukee, WI 53202", Bathrooms = 2, Bedrooms = 3, City = "Milwaukee", Occupied = false, Rent = 500, State = "Wisconsin", ZipCode = "53202" },
                new Property() { ID = 2, AcceptsCats = true, AcceptsDogs = true, Address = "231 W Wisconsin Ave Milwaukee, WI 53203", Bathrooms = 1, Bedrooms = 2, City = "Milwaukee", Occupied = false, Rent = 900, State = "Wisconsin", ZipCode = "53203" }
                );
            db.SaveChanges();
        }
    }
}
