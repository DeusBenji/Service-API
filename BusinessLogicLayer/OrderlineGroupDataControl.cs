using AutoMapper;
using Service_Api.BusinessLogicLayer.Interfaces;
using Service_Api.DTOs;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service_Api.BusinessLogicLayer
{
    public class OrderlineGroupDataControl : IOrderlineGroupData
    {
        private readonly IOrderlineGroup _orderlineGroupDatabaseAccess;
        private readonly IMapper _mapper;

        public OrderlineGroupDataControl(IOrderlineGroup orderlineGroupDatabaseAccess, IMapper mapper)
        {
            _orderlineGroupDatabaseAccess = orderlineGroupDatabaseAccess;
            _mapper = mapper;
        }

        public async Task<OrderlineGroupDto> GetOrderlineGroupById(int id)
        {
            var orderlineGroup = await _orderlineGroupDatabaseAccess.GetOrderlineGroupById(id);
            return _mapper.Map<OrderlineGroupDto>(orderlineGroup);
        }

        public async Task<List<OrderlineGroupDto>> GetAllOrderlineGroups()
        {
            var orderlineGroups = await _orderlineGroupDatabaseAccess.GetAllOrderlineGroups();
            return _mapper.Map<List<OrderlineGroupDto>>(orderlineGroups);
        }

        public async Task<int> CreateOrderlineGroup(OrderlineGroupDto orderlineGroupDto)
        {
            var orderlineGroup = _mapper.Map<OrderlineGroup>(orderlineGroupDto);
            return await _orderlineGroupDatabaseAccess.CreateOrderlineGroup(orderlineGroup);
        }

        public async Task<bool> UpdateOrderlineGroup(OrderlineGroupDto orderlineGroupDto)
        {
            var orderlineGroup = _mapper.Map<OrderlineGroup>(orderlineGroupDto);
            return await _orderlineGroupDatabaseAccess.UpdateOrderlineGroupById(orderlineGroup);
        }

        public async Task<bool> DeleteOrderlineGroup(int id)
        {
            return await _orderlineGroupDatabaseAccess.DeleteOrderlineGroupById(id);
        }
    }
}
