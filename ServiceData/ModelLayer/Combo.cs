using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.ModelLayer
{
    public class Combo
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageName { get; set; }

        public decimal ComboPrice { get; set; }

      
        //Constructor without parameters
        public Combo() { }

        //Reuses consturctor without Id
        public Combo(string name, string imageName, decimal comboPrice)
        {
            Name = name;
            ImageName = imageName;
            ComboPrice = comboPrice;
        }
        //Reuses constructor with Id
        public Combo(int id, string name, string imageName, decimal comboPrice) : this (name, imageName, comboPrice)
        {
            Id = id;
        }

    }
}
