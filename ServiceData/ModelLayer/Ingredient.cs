using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.ModelLayer
{
    public class Ingredient
    {
        public string Name { get; set; }
        public decimal IngredientPrice { get; set; }
        public string ImageName { get; set; }
        public int Id { get; set; }
        //Empty Constructor
        public Ingredient() { }

        //Constructor with parameters
        public Ingredient(string name, decimal ingredientPrice, string imageName)
        {
            Name = name;
            IngredientPrice = ingredientPrice;
            ImageName = imageName;
        }

        //reuses constructor with Id
        public Ingredient(int id, string name, decimal ingredientPrice, string imageName) : this(name, ingredientPrice, imageName)
        {
            Id = id;
        }

    }
}