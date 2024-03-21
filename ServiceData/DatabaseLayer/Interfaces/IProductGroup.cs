using ServiceData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer.Interfaces
{
    public interface IProductGroup
    {
        Task<ProductGroup> GetProductGroupById(int id);
        Task<List<ProductGroup>> GetAllProductGroups();
        Task<int> CreateProductGroup(ProductGroup aProductGroup);
        Task<bool> DeleteProductGroupById(int id);
        Task<bool> UpdateProductGroupById(ProductGroup productGroupToUpdate);
    }
}
