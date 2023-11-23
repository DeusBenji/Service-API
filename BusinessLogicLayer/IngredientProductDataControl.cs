using AutoMapper;
using Service_Api.BusinessLogicLayer.Interfaces;
using Service_Api.DTOs;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service_Api.BusinessLogicLayer
{
    public class IngredientProductDataControl : IIngredientProductData
    {
        private readonly IIngredientProduct _ingredientProductDatabaseAccess;
        private readonly IMapper _mapper;

        public IngredientProductDataControl(IIngredientProduct ingredientProductDatabaseAccess, IMapper mapper)
        {
            _ingredientProductDatabaseAccess = ingredientProductDatabaseAccess;
            _mapper = mapper;
        }

        public async Task<IngredientProductDto> GetIngredientProductByIds(int productId, int ingredientId)
        {
            var ingredientProduct = await _ingredientProductDatabaseAccess.GetIngredientProductByIds(productId, ingredientId);
            return _mapper.Map<IngredientProductDto>(ingredientProduct);
        }

        public async Task<List<IngredientProductDto>> GetAllIngredientProducts()
        {
            var ingredientProducts = await _ingredientProductDatabaseAccess.GetAllIngredientProducts();
            return _mapper.Map<List<IngredientProductDto>>(ingredientProducts);
        }

        public async Task CreateIngredientProduct(IngredientProductDto ingredientProductDto)
        {
            var ingredientProduct = _mapper.Map<IngredientProduct>(ingredientProductDto);
            await _ingredientProductDatabaseAccess.CreateIngredientProduct(ingredientProduct);
        }

        public async Task<bool> UpdateIngredientProduct(IngredientProductDto ingredientProductDto)
        {
            var ingredientProduct = _mapper.Map<IngredientProduct>(ingredientProductDto);
            return await _ingredientProductDatabaseAccess.UpdateIngredientProductByIds(ingredientProduct);
        }

        public async Task<bool> DeleteIngredientProduct(int productId, int ingredientId)
        {
            return await _ingredientProductDatabaseAccess.DeleteIngredientProductByIds(productId, ingredientId);
        }
    }
}
