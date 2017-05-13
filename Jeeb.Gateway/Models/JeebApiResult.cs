using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jeeb.Gateway.Models
{
    internal  class JeebApiResult<TModel>
    {
        public TModel Result { set; get; }

        public bool HasError { set; get; }

        public int? ErrorCode { set; get; }

        public string ErrorMessage { set; get; }

    }
}
