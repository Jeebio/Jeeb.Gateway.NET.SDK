using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jeeb.Gateway.Models
{
    public class CurrencyConvertRequest
    {
        public string BaseCurrency { set; get; }

        public string TargetCurrency { set; get; }

        public decimal Value { set; get; }

    }
}
