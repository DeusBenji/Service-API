using ServiceData.ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer.Interfaces
{
    public interface IDiscount
    {
        Task<Discount> GetDiscountById(int id);
        Task<List<Discount>> GetAllDiscount();
        Task<int> CreateDiscount(Discount anDiscount);
        Task<bool> DeleteDiscountById(int id);
        Task<bool> UpdateDiscountById(Discount DiscountToUpdate);
    }
}
