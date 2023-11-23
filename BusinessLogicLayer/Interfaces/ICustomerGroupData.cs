using Service_Api.BusinessLogicLayer;
using Service_Api.DTOs;
using System.Collections.Generic;

namespace Service_Api.BusinessLogicLayer.Interfaces
{
    public interface ICustomerGroupData
    {
        CustomerGroupDto GetCustomerGroupById(int id);
        List<CustomerGroupDto> GetAllCustomerGroups();
        int CreateCustomerGroup(CustomerGroupDto customerGroupDto);
        bool UpdateCustomerGroupById(int id, CustomerGroupDto customerGroupDto);
        bool DeleteCustomerGroupById(int id);
    }
}
