using AutoMapper;
using Service_Api.BusinessLogicLayer.Interfaces;
using Service_Api.DTOs;
using ServiceData.DatabaseLayer;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;

namespace Service_Api.BusinessLogicLayer
{
    public class IngredientDataControl : IIngredientData
    {
        private readonly IIngredient _ingredientDatabaseAccess;
        private readonly IMapper _mapper;

        public IngredientDataControl(IIngredient ingredientDatabaseAccess, IMapper mapper)
        {
            _ingredientDatabaseAccess = ingredientDatabaseAccess;
            _mapper = mapper;
        }

        public async Task<IngredientDto> GetIngredientById(int id)
        {
            var ingredient = await _ingredientDatabaseAccess.GetIngredientById(id);
            return _mapper.Map<IngredientDto>(ingredient);
        }

        public async Task<List<IngredientDto>> GetAllIngredients()
        {
            var ingredients = await _ingredientDatabaseAccess.GetAllIngredients();
            return _mapper.Map<List<IngredientDto>>(ingredients);
        }

        public async Task<int> CreateIngredient(IngredientDto ingredientDto)
        {
            var ingredient = _mapper.Map<Ingredient>(ingredientDto);
            return await _ingredientDatabaseAccess.CreateIngredient(ingredient);
        }

        public async Task<bool> UpdateIngredientById(int id, IngredientDto ingredientDto)
        {
            var ingredient = _mapper.Map<Ingredient>(ingredientDto);
            ingredient.Id = id; // Set the ID for the update
            return await _ingredientDatabaseAccess.UpdateIngredientById(ingredient);
        }

        public async Task<bool> DeleteIngredientById(int id)
        {
            return await _ingredientDatabaseAccess.DeleteIngredientById(id);
        }
        public async Task<List<IngredientDto>> GetIngredientsByProductId(int productId)
        {
            var ingredients = await _ingredientDatabaseAccess.GetIngredientsByProductId(productId);
            return _mapper.Map<List<IngredientDto>>(ingredients);
        }
    }

}

