using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.ModelLayer
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductNumber { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public int Barcode { get; set; }
        public enum Category {Burgere, Salater, Sides, Dips, Drikkevarer}
        public Category CategoryType { get; set; }
        public int ProductGroup { get; set; }
        public string ImageName {  get; set; }

        //Empty Constructor
        public Product() { }

        //Constructor with parameters
        public Product(string productNumber, string description, decimal basePrice, int barcode, Category categoryType, int productGroup, string imageName)
        {
            ProductNumber = productNumber;
            Description = description;
            BasePrice = basePrice;
            Barcode = barcode;
            CategoryType = categoryType;
            ProductGroup = productGroup;
            ImageName = imageName;  
        }

        //Reuses constructor with Id
        public Product(int id, string productNumber, string description, decimal basePrice, int barcode, Category categoryType, int productGroup, string imageName) : this(productNumber, description, basePrice, barcode, categoryType, productGroup, imageName)
        {
            Id = id;
        }

    }
}
