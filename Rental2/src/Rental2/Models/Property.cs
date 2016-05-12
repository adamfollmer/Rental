using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rental2.Models
{
    public class Property
    {
        public Property()
        {
            PastRentals = new HashSet<RentalUserConnection>();
        }
        public int ID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public double Bedrooms { get; set; }
        public double Bathrooms { get; set; }
        public bool AcceptsCats { get; set; }
        public bool AcceptsDogs { get; set; }
        [DataType(DataType.Currency)]
        public double Rent { get; set; }

        public virtual ICollection<RentalUserConnection> PastRentals { get; set; }
        
    }
}
