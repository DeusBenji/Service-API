using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.ModelLayer
{
    public class Image
    {
        public string FileName { get; set; }

        public IFormFile file { get; set; }

    }
}