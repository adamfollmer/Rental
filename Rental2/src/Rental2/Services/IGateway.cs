using Rental2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rental2.Services
{
    public interface IGateway
    {
        PaymentResult ProcessPayment(PaymentViewModel model);
    }
}
