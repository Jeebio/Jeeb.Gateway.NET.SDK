using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jeeb.Gateway.Models
{
    public class CallBack
    {
        public string NotificationUrl { set; get; }

        public string CallBackUrl { set; get; }

        public decimal? Value { set; get; }

        public byte StateId { set; get; }

        public decimal RequestAmount { set; get; }

        public string OrderNo { set; get; }

        public string ReferenceNo { set; get; }
    }
}
