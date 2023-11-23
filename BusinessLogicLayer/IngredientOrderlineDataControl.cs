using AutoMapper;
using Service_Api.BusinessLogicLayer.Interfaces;
using Service_Api.DTOs;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service_Api.BusinessLogicLayer
{
    public class IngredientOrderlineDataControl : IIngredientOrderlineData
    {
        private readonly IIngredientOrderline _ingredientOrderlineDatabaseAccess;
        private readonly IMapper _mapper;

        public IngredientOrderlineDataControl(IIngredientOrderline ingredientOrderlineDatabaseAccess, IMapper mapper)
        {
            _ingredientOrderlineDatabaseAccess = ingredientOrderlineDatabaseAccess;
            _mapper = mapper;
        }

        public async Task<IngredientOrderlineDto> GetIngredientOrderlineById(int ingredientId, int orderlineId)
        {
            var ingredientOrderline = await _ingredientOrderlineDatabaseAccess.GetIngredientOrderlineByIds(ingredientId, orderlineId);
            return _mapper.Map<IngredientOrderlineDto>(ingredientOrderline);
        }

        public async Task<List<IngredientOrderlineDto>> GetAllIngredientOrderlines()
        {
            var ingredientOrderlines = await _ingredientOrderlineDatabaseAccess.GetAllIngredientOrderlines();
            return _mapper.Map<List<IngredientOrderlineDto>>(ingredientOrderlines);
        }

        public async Task CreateIngredientOrderline(IngredientOrderlineDto ingredientOrderlineDto)
        {
            var ingredientOrderline = _mapper.Map<IngredientOrderline>(ingredientOrderlineDto);
            await _ingredientOrderlineDatabaseAccess.CreateIngredientOrderline(ingredientOrderline);
        }

        public async Task<bool> UpdateIngredientOrderlineById(IngredientOrderlineDto ingredientOrderlineDto)
        {
            var ingredientOrderlineToUpdate = _mapper.Map<IngredientOrderline>(ingredientOrderlineDto);
            return await _ingredientOrderlineDatabaseAccess.UpdateIngredientOrderlineByIds(ingredientOrderlineToUpdate);
        }

        public async Task<bool> DeleteIngredientOrderlineById(int ingredientId, int orderlineId)
        {
            return await _ingredientOrderlineDatabaseAccess.DeleteIngredientOrderlineByIds(ingredientId, orderlineId);
        }
        
   
    }
}
