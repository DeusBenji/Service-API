using ServiceData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer.Interfaces
{
    public interface ICustomerGroup
    {
        CustomerGroup GetCustomerGroupById(int id);
        List<CustomerGroup> GetAllCustomerGroup();
        int CreateCustomerGroup(CustomerGroup anCustomerGroup);
        bool DeleteCustomerGroupById(int id);
        bool UpdateCustomerGroupById(CustomerGroup customerGroupToUpdate);


    }
}
