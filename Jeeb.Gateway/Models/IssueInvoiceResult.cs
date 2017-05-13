using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jeeb.Gateway.Models
{
    public class IssueInvoice
    {
        public string Token { set; get; }

        public string ReferenceNo { get; set; }

        public string Addr { get; set; }

        public DateTime ExpirationTime { set; get; }
        
    }
}
