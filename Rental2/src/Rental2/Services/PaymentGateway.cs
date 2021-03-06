﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rental2.Models;
using Braintree;

namespace Rental2.Services
{
    public class PaymentGateway : IGateway
    {
        private IBraintreeGateway BraintreeGateway { get; set; }
        private readonly BraintreeGateway _gateway = new BraintreeGateway
        {
            Environment = Braintree.Environment.SANDBOX,
            MerchantId = "gq92xz2nnnmvxhgt",
            PublicKey = "y5scdh72v5hr7gdw",
            PrivateKey = "1656e4db305b62b883dd3f58073b9a01"
        };

        public IBraintreeGateway GetGateway()
        {
            if(BraintreeGateway == null)
            {
                BraintreeGateway = _gateway;
            }
            return BraintreeGateway;
        }

        //public PaymentResult ProcessPayment(Payment model)
        //{
        //    var request = new TransactionRequest()
        //    {
        //        Amount = model.TotalPayment,
        //        CreditCard = new TransactionCreditCardRequest()
        //        {
        //            Number = model.CreditCardNumber,
        //            CVV = model.Cvv,
        //            ExpirationMonth = model.Month,
        //            ExpirationYear = model.Year,
        //            CardholderName = model.Name
        //        },
        //        Options = new TransactionOptionsRequest()
        //        {
        //            SubmitForSettlement = true
        //        }
        //    };
        //    var result = _gateway.Transaction.Sale(request);
        //    if (result.IsSuccess())
        //    {
        //        return new PaymentResult(result.Target.Id, true, null);
        //    }
        //    return new PaymentResult(null, false, result.Message);
        //}
    }
}
