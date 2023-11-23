using AutoMapper;
using Service_Api.BusinessLogicLayer.Interfaces;
using Service_Api.DTOs;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System.Collections.Generic;

namespace Service_Api.BusinessLogicLayer
{
    public class CustomerGroupDataControl : ICustomerGroupData
    {
        private readonly ICustomerGroup _customerGroupDatabaseAccess;
        private readonly IMapper _mapper;

        public CustomerGroupDataControl(ICustomerGroup customerGroupDatabaseAccess, IMapper mapper)
        {
            _customerGroupDatabaseAccess = customerGroupDatabaseAccess;
            _mapper = mapper;
        }

        public CustomerGroupDto GetCustomerGroupById(int id)
        {
            var customerGroup = _customerGroupDatabaseAccess.GetCustomerGroupById(id);
            return _mapper.Map<CustomerGroupDto>(customerGroup);
        }

        public List<CustomerGroupDto> GetAllCustomerGroups()
        {
            var customerGroups = _customerGroupDatabaseAccess.GetAllCustomerGroup();
            return _mapper.Map<List<CustomerGroupDto>>(customerGroups);
        }

        public int CreateCustomerGroup(CustomerGroupDto customerGroupDto)
        {
            var customerGroup = _mapper.Map<CustomerGroup>(customerGroupDto);
            return _customerGroupDatabaseAccess.CreateCustomerGroup(customerGroup);
        }

        public bool UpdateCustomerGroupById(int id, CustomerGroupDto customerGroupDto)
        {
            var customerGroup = _mapper.Map<CustomerGroup>(customerGroupDto);
            customerGroup.Id = id; // Set the ID for the update
            return _customerGroupDatabaseAccess.UpdateCustomerGroupById(customerGroup);
        }

        public bool DeleteCustomerGroupById(int id)
        {
            return _customerGroupDatabaseAccess.DeleteCustomerGroupById(id);
        }
    }
}
