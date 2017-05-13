using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jeeb.Gateway.Models
{
    public class InvoiceStatusResult
    {
        public DateTime? FinalizedTime { set; get; }

        public DateTime ExpirationTime { set; get; }

        public byte StateId { set; get; }

        public decimal? Value { set; get; }

        public decimal RequestAmount { set; get; }

        public string OrderNo { set; get; }

        public string ReferenceNo { set; get; }

        public bool IsConfirmed { set; get; }
    }
}
