using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeeb.Gateway.Demo.Models
{
    public class NewPaymentViewModel
    {
        public string Token { set; get; }

        public string CallBackUrl { set; get; }

        public string NotificationUrl { set; get; }

        public decimal RequestAmount { set; get; }

        public bool AllowReject { set; get; }

        public string OrderNo { set; get; }
    }
}
