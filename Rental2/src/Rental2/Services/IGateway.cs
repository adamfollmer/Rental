using Rental2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Braintree;

namespace Rental2.Services
{
    public interface IGateway
    {
        IBraintreeGateway GetGateway();
    }
}
