using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rental2.Models
{
    public class Payment
    {
        public int ID { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Payment Amount")]
        public decimal PaymentAmount { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Payment Due Date")]
        public DateTime DueDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Payment Received")]
        public DateTime DateReceived { get; set; }

        public int YearlyRentalID { get; set; }
        public virtual YearlyRental YearlyRental { get; set; }


    }
}
