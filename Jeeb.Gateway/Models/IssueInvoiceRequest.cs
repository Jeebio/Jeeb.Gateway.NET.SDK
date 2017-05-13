using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jeeb.Gateway.Models
{
    public class IssueInvoiceRequest
    {
        public  string OrderNo { set; get; }

        public decimal RequestAmount { set; get; }

        public string NotificationUrl { set; get; }

        public string CallBackUrl { set; get; }

        public bool AllowReject { set; get; }

    }
}
