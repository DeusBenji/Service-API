using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.ModelLayer
{
    public class CustomerGroup
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        //Empty constructor
        public CustomerGroup() { }

        //Constructor with parameter, except Id
        public CustomerGroup(string name)
        {
            Name = name;
        }

        //Reuses constructor with Id
        public CustomerGroup(int id, string name) : this (name)
        {
            Id = id;
        }

    }
}
