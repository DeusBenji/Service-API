using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.ModelLayer
{
    public class Orders 
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public DateTime DateTime { get; set; }
        public decimal TotalPrice { get; set; }
        public int ShopId { get; set; }
        //Empty Constructor
        public Orders() { }

        //Constructor with parameters
        public Orders( int orderNumber, DateTime dateTime, decimal totalPrice, int shopId)
        {
            
            OrderNumber = orderNumber;
            DateTime = dateTime;
            TotalPrice = totalPrice;
            ShopId = shopId;
        }

        //Constructor with Id
        public Orders(int id, int orderNumber, DateTime dateTime, decimal totalPrice, int shopId) : this (orderNumber, dateTime, totalPrice, shopId)
        {
            Id = id;
        }

    }
}
