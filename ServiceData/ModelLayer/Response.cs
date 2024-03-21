using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.ModelLayer
{
    public class Response
    {
        public int StatusCode { get; set; }

        public string ErrorMessage { get; set; }
    }
}
