using Service_Api.DTOs;
using ServiceData.ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service_Api.BusinessLogicLayer.Interfaces
{
    public interface IProductData
    {
        Task<ProductDto> GetProductById(int id);
        Task<List<ProductDto>> GetAllProducts();
        Task<int> CreateProduct(ProductDto productDto);
        Task<bool> UpdateProductById(int id, ProductDto productDto);
        Task<bool> DeleteProductById(int id);
        Task<List<ProductDto>> GetProductsByShopId(int shopId);
    }
}
