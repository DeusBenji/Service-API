using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.ModelLayer
{
    public class OrderlineGroup
    {
        public int ProductId { get; set; }
        public int OrderlineId { get; set; }
        public int ComboId { get; set; }
        //Empty constructor
        public OrderlineGroup() { }

        //Constructor with parameteter xecept orderline
       public OrderlineGroup(int productId, int orderlineId, int comboId)
        {
            ProductId = productId;
            OrderlineId = orderlineId;
            ComboId = comboId;

        }


    }
}
