using Service_Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service_Api.BusinessLogicLayer.Interfaces
{
    public interface IOrderlineGroupData
    {
        Task<OrderlineGroupDto> GetOrderlineGroupById(int id);
        Task<List<OrderlineGroupDto>> GetAllOrderlineGroups();
        Task<int> CreateOrderlineGroup(OrderlineGroupDto orderlineGroupDto);
        Task<bool> UpdateOrderlineGroup(OrderlineGroupDto orderlineGroupDto);
        Task<bool> DeleteOrderlineGroup(int id);
    }
}
