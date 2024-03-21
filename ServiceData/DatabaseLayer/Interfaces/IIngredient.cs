using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceData.ModelLayer;

namespace ServiceData.DatabaseLayer.Interfaces
{
    public interface IIngredient
    {
        Task<Ingredient> GetIngredientById(int id);
        Task<List<Ingredient>> GetAllIngredients();
        Task<int> CreateIngredient(Ingredient anIngredient);
        Task<bool> DeleteIngredientById(int id);
        Task<bool> UpdateIngredientById(Ingredient ingredientToUpdate);
        Task<List<Ingredient>> GetIngredientsByProductId(int productId);
    }
}
