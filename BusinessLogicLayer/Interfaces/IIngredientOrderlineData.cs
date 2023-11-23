using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Service_Api.DTOs;
using ServiceData.ModelLayer;
using ServiceData.DatabaseLayer.Interfaces;

namespace Service_Api.BusinessLogicLayer.Interfaces
{
    public interface IIngredientOrderlineData
    {
        Task<IngredientOrderlineDto> GetIngredientOrderlineById(int ingredientId, int OrderlineId);
        Task<List<IngredientOrderlineDto>> GetAllIngredientOrderlines();
        Task CreateIngredientOrderline(IngredientOrderlineDto ingredientOrderlineDto);
        Task<bool> UpdateIngredientOrderlineById(IngredientOrderlineDto ingredientOrderlineDto);
        Task<bool> DeleteIngredientOrderlineById(int ingredientId, int OrderlineId);
    }
}
