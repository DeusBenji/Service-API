using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceData.ModelLayer;

namespace ServiceData.DatabaseLayer.Interfaces
{
    public interface IShopProduct
    {
        Task<ShopProduct> GetShopProductByIds(int shopId, int productId);
        Task<List<ShopProduct>> GetAllShopProducts();
        Task CreateShopProduct(ShopProduct shopProduct);
        Task<bool> DeleteShopProductByIds(int shopId, int productId);
        Task<bool> UpdateShopProductByIds(ShopProduct shopProductToUpdate);
        Task<List<ShopProduct>> GetShopProductByShopId(int shopId);
    }
}
