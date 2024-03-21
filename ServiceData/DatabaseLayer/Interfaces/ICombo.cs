using ServiceData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer.Interfaces
{
    public interface ICombo
    {
        Task<Combo> GetComboById(int id);
        Task<List<Combo>> GetAllCombos();
        Task<int> CreateCombo(Combo aCombo);
        Task<bool> DeleteComboById(int id);
        Task<bool> UpdateComboById(Combo comboToUpdate);
        Task<List<Combo>> GetCombosByShopId(int shopId);
    }
}
