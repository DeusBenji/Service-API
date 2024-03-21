using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.ModelLayer
{
    public class Shop
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public enum Storetype { Restaurant, FoodStand, CandyStore }
        public Storetype Type { get; set; }

        //Empty Constructor
        public Shop() { }

        //Constructor with parameters
        public Shop(string name, string location, Storetype type)
        {
            Name = name;
            Location = location;
            Type = type;
        }

        //Reuses constructor with Id
        public Shop(int id, string name, string location, Storetype type) : this(name, location, type)
        {
            Id = id;
        }

    }
}
