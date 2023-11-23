using Service_Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service_Api.BusinessLogicLayer.Interfaces
{
    public interface IIngredientProductData
    {
        Task<IngredientProductDto> GetIngredientProductByIds(int productId, int ingredientId);
        Task<List<IngredientProductDto>> GetAllIngredientProducts();
        Task CreateIngredientProduct(IngredientProductDto ingredientProductDto);
        Task<bool> UpdateIngredientProduct(IngredientProductDto ingredientProductDto);
        Task<bool> DeleteIngredientProduct(int productId, int ingredientId);
    }
}
