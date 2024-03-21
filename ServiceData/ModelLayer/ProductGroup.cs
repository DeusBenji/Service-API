using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.ModelLayer
{
    public class ProductGroup
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        //Empty constructor
        public ProductGroup() { }

        //Constructor with parameter, except Id
        public ProductGroup(string name)
        {
            Name = name;
        }

        //Reuses constructor with Id
        public ProductGroup(int id, string name) : this(name)
        {
            Id = id;
        }

    }
}
