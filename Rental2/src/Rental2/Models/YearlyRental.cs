using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rental2.Models
{
    public class YearlyRental
    {
        public YearlyRental()
        {
            Tenants = new HashSet<RentalUserConnection>();
            Bills = new HashSet<Bill>();
            Payments = new HashSet<Payment>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Range(10000,99999)]
        [Display(Name = "Yearly Rental ID")]
        public int ID { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Move-in Date")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Move-out Date")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "Tenants")]
        public ICollection<RentalUserConnection> Tenants { get; set; }
        [Display(Name = "Property")]
        public int PropertyID { get; set; }
        public virtual Property Property { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
