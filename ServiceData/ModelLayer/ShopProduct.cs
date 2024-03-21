using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.ModelLayer
{
    public class ShopProduct
    {
        public int ShopId { get; set; }
        public int ProductId { get; set; }

        //Empty constructor
        public ShopProduct()
        {

        }

        //Constructor with parameters
        public ShopProduct(int shopId, int productId)
        {
            ShopId = shopId;
            ProductId = productId;
        }

    }
}
