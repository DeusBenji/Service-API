using Service_Api.DTOs;

namespace Service_Api.BusinessLogicLayer.Interfaces
{
    public interface IComboData
    {
        Task<ComboDto> GetComboById(int id);
        Task<List<ComboDto>> GetAllCombos();
        Task<int> CreateCombo(ComboDto comboDto);
        Task<bool> UpdateComboById(int id, ComboDto comboDto);
        Task<bool> DeleteComboById(int id);
    }
}
