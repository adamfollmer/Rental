﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Rental2.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            RentalHistory = new HashSet<RentalUserConnection>();
        } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Display(Name = "Forwarding Address")]
        public string ForwardingAddress { get; set; }
        public virtual ICollection<RentalUserConnection> RentalHistory { get; set; }
    }
}
