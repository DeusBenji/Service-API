using AutoMapper;
using Service_Api.BusinessLogicLayer.Interfaces;
using Service_Api.DTOs;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service_Api.BusinessLogicLayer
{
    public class ComboDataControl : IComboData
    {
        private readonly ICombo _comboDatabaseAccess;
        private readonly IMapper _mapper;

        public ComboDataControl(ICombo comboDatabaseAccess, IMapper mapper)
        {
            _comboDatabaseAccess = comboDatabaseAccess;
            _mapper = mapper;
        }

        public async Task<ComboDto> GetComboById(int id)
        {
            var combo = await _comboDatabaseAccess.GetComboById(id);
            return _mapper.Map<ComboDto>(combo);
        }

        public async Task<List<ComboDto>> GetAllCombos()
        {
            var combos = await _comboDatabaseAccess.GetAllCombos();
            return _mapper.Map<List<ComboDto>>(combos);
        }

        public async Task<int> CreateCombo(ComboDto comboDto)
        {
            var combo = _mapper.Map<Combo>(comboDto);

            return await _comboDatabaseAccess.CreateCombo(combo);
        }

        public async Task<bool> UpdateComboById(int id, ComboDto comboDto)
        {
            var combo = _mapper.Map<Combo>(comboDto);
            combo.Id = id; // Set the ID for the update
            return await _comboDatabaseAccess.UpdateComboById(combo);
        }

        public async Task<bool> DeleteComboById(int id)
        {
            return await _comboDatabaseAccess.DeleteComboById(id);
        }
    }
}
