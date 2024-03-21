using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.ModelLayer
{
    public class IngredientProduct
    {
        public int ProductId { get; set; }
        public int IngredientId { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int Count { get; set; }
        //Empty constructor
        public IngredientProduct() { }
        //Constructor with parameters
        public IngredientProduct(int productId, int ingredientId, int min, int max, int count)
        {
            ProductId = productId;
            IngredientId = ingredientId;
            Min = min;
            Max = max;
            Count = count;
        }



    }
}
