using ServiceData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer.Interfaces
{
    public interface IIngredientOrderline
    {
        Task<IngredientOrderline> GetIngredientOrderlineByIds(int ingredientId, int orderlineId);
        Task<List<IngredientOrderline>> GetAllIngredientOrderlines();
        Task CreateIngredientOrderline(IngredientOrderline anIngredientOrderline);
        Task<bool> DeleteIngredientOrderlineByIds(int ingredientId, int orderlineId);
        Task<bool> UpdateIngredientOrderlineByIds(IngredientOrderline ingredientOrderlineToUpdate);
    }
}
