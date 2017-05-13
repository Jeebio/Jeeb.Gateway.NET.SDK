using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jeeb.Gateway.Models
{
    public class ConfirmInvoiceRequest
    {
        /// <summary>
        /// Payment token
        /// </summary>
        public string Token { set; get; }
    }
}
