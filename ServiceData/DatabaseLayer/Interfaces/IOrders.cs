using ServiceData.ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer.Interfaces
{
    public interface IOrders
    {
        Task<Orders> GetOrderById(int id);
        Task<List<Orders>> GetAllOrders();
        Task<int> CreateOrder(Orders aOrder);
        Task<bool> DeleteOrderById(int id);
        Task<bool> UpdateOrderById(Orders orderToUpdate);
    }
}
