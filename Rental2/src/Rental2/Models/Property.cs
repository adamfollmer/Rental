using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rental2.Models
{
    public class Property
    {
        public int ID { get; set; }
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public double Rent { get; set; }

        public virtual IList<YearlyRental> PastRentals { get; set; }
    }
}
