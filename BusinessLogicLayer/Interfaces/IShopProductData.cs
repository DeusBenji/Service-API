using Service_Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service_Api.BusinessLogicLayer.Interfaces
{
    public interface IShopProductData
    {
        Task CreateShopProduct(ShopProductDto shopProductDto);
        Task<bool> DeleteShopProductByIds(int shopId, int productId);
        Task<List<ShopProductDto>> GetAllShopProducts();
        Task<ShopProductDto> GetShopProductByIds(int shopId, int productId);
        Task<bool> UpdateShopProductByIds(int shopId, int productId, ShopProductDto shopProductDto);
        Task<List<ShopProductDto>> GetShopProductsByShopId(int shopId);
    }
}
