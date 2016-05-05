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
        [Display(Name = "Name On Card")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Postal Code")]
        [StringLength(10, MinimumLength = 5)]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }
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
