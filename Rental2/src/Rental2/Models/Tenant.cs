using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rental2.Models
{
    public class Tenant
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Forwarding Address")]
        public string ForwardingAddress { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Email address is not valid")]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber, ErrorMessage = "Phone number is not valid")]
        [Required]
        public string Phone { get; set; }

        public virtual ICollection<YearlyRental> RentalHistory { get; set; }
    }

}
