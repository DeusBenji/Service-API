using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.ModelLayer
{
    public class IngredientOrderline
    {
        public int IngredientId { get; set; }
        public int OrderlineId { get; set; }
        public int Delta { get; set; }

        //Empty Constructor
        public IngredientOrderline() { }
        //Constructor with parameters
        public IngredientOrderline(int ingredientId, int orderlineId, int delta)
        {
            IngredientId = ingredientId;
            OrderlineId = orderlineId;
            Delta = delta;
        }


    }
}
