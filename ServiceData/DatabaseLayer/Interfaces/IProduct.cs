using ServiceData.ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer.Interfaces
{
    public interface IProduct
    {
        Task<Product> GetProductById(int id);
        Task<List<Product>> GetAllProducts();
        Task<int> CreateProduct(Product aProduct);
        Task<bool> DeleteProductById(int id);
        Task<bool> UpdateProductById(Product productToUpdate);
        Task<List<Product>> GetProductsByShopId(int shopId);
        Task<List<string>> GetAllCategories();
        Task<List<Product>> GetProductsByCategoryAndShop(string category, int shopId);
    }
}
