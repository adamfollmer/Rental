using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rental2.Models
{
    public class Payment
    {
        public Payment()
        {
            DateTimeReceived = DateTime.Now;
        }
        public int ID { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Payment Amount")]
        public int PaymentAmount { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Payment Received")]
        public DateTime? DateTimeReceived { get; set; }
        public string Description { get; set; }
        public int BillId { get; set; }
        public virtual Bill Bill { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser Tenant { get; set; }
    }
}
