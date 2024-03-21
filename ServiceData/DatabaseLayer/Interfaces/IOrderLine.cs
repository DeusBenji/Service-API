using ServiceData.ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer.Interfaces
{
    public interface IOrderLine
    {
        Task<OrderLine> GetOrderLineById(int id);
        Task<List<OrderLine>> GetAllOrderLines();
        Task<int> CreateOrderLine(OrderLine orderLine);
        Task<bool> DeleteOrderLineById(int id);
        Task<bool> UpdateOrderLineById(OrderLine orderLineToUpdate);
        Task<OrderlineGroup> GetOrderlineGroupByOrderlineId(int orderlineId);
    }
}
