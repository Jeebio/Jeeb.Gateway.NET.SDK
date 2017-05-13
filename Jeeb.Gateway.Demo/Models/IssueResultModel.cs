using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jeeb.Gateway.Demo.Models
{
    public class IssueResultModel
    {
        public string ReferenceNo { get; set; }
        public string Addr { get; set; }

        public DateTime ExpirationTime { set; get; }
    }
}