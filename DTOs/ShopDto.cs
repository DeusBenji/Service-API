using System;
using System.Collections.Generic;
using System.Text;

namespace Service_Api.DTOs
{
    public class ShopDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }

        //Empty Constructor
        public ShopDto() { }
    }
}
