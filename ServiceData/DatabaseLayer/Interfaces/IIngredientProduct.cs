using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceData.ModelLayer;

namespace ServiceData.DatabaseLayer.Interfaces
{
    public interface IIngredientProduct
    {
        Task<IngredientProduct> GetIngredientProductByIds(int productId, int ingredientId);
        Task<List<IngredientProduct>> GetAllIngredientProducts();
        Task CreateIngredientProduct(IngredientProduct ingredientProduct);
        Task<bool> DeleteIngredientProductByIds(int productId, int ingredientId);
        Task<bool> UpdateIngredientProductByIds(IngredientProduct ingredientProductToUpdate);
    }
}
