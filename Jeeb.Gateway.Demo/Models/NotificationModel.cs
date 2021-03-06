﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeeb.Gateway.Demo.Models
{
    public class NotificationModel
    {
        public string NotificationUrl { set; get; }

        public decimal? Value { set; get; }

        public byte StateId { set; get; }

        public decimal RequestAmount { set; get; }

        public string OrderNo { set; get; }

        public string ReferenceNo { set; get; }

    }
}
