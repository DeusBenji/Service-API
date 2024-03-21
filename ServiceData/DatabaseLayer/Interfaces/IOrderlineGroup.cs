using ServiceData.ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer.Interfaces
{
    public interface IOrderlineGroup
    {
        Task CreateOrderlineGroup(OrderlineGroup orderlineGroup);
        Task<List<OrderlineGroup>> GetAllOrderlineGroups();
        Task<bool> DeleteOrderlineGroup(int orderlineID, int productId, int comboId);
        Task<OrderlineGroup> GetOrderlineGroupById(int orderlineID, int productId, int comboId);
        Task<bool> UpdateOrderlineGroupById(OrderlineGroup orderlineGroupToUpdate);
    }
}
