using Service_Api.DTOs;

namespace Service_Api.BusinessLogicLayer.Interfaces
{
    public interface IProductGroupData
    {
        Task<ProductGroupDto> GetProductGroupById(int id);
        Task<List<ProductGroupDto>> GetAllProductGroups();
        Task<int> CreateProductGroup(ProductGroupDto productGroupDto);
        Task<bool> UpdateProductGroupById(int id, ProductGroupDto productGroupDto);
        Task<bool> DeleteProductGroupById(int id);
    }
}
