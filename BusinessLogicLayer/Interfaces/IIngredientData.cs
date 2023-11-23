using Service_Api.DTOs;
using ServiceData.ModelLayer;

namespace Service_Api.BusinessLogicLayer.Interfaces
{
    public interface IIngredientData
    {
        Task<IngredientDto> GetIngredientById(int id);
        Task<List<IngredientDto>> GetAllIngredients();
        Task<int> CreateIngredient(IngredientDto ingredientDto);
        Task<bool> UpdateIngredientById(int id, IngredientDto ingredientDto);
        Task<bool> DeleteIngredientById(int id);
        Task<List<IngredientDto>> GetIngredientsByProductId(int productId);
    }
}

