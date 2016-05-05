using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Rental2.ViewModels
{
    public class PaymentViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public decimal TotalPayment { get; set; }
        [Required]
        public string CreditCardNumber { get; set; }
        [Required]
        public string Cvv { get; set; }
        [Required]
        public string Month { get; set; }
        [Required]
        public string Year { get; set; }

    }
}
