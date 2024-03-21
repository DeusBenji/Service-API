using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.ModelLayer
{
    public class Discount
    {
        public int Id { get; set; }
        public decimal Rate { get; set; }
        public int ProductGroupId { get ; set; }
        public int CustomerGroupId { get; set; }


        //Empty Constructor
        public Discount() { }
    
        //Constructor without Id Parameter
        public Discount(decimal rate, int productGroupId, int customerGroupId )
        {

            Rate = rate;
            ProductGroupId = productGroupId;
            CustomerGroupId = customerGroupId;

        }

        //Reuses Constructor

        public Discount(int id, decimal rate, int productGroupId, int customerGroupId) : this (rate, productGroupId, customerGroupId)
        {
            Id = id;
        }


    }



}
