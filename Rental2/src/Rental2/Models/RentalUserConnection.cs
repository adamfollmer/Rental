using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rental2.Models
{
    public class RentalUserConnection
    {
        [Key]
        [ForeignKey("AspNetUsers")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser Tenant { get; set; }
        [Key]
        [ForeignKey("YearlyRental")]
        public int YearlyRentalId { get; set; }
        public YearlyRental YearlyRental { get; set; }
    }
}
