using Service_Api.DTOs;
using System.Threading.Tasks;

namespace Service_Api.BusinessLogicLayer.Interfaces
{
    public interface IDiscountData
    {
        Task<DiscountDto> GetDiscountById(int id);
        Task<List<DiscountDto>> GetAllDiscounts();
        Task<int> CreateDiscount(DiscountDto discountDto);
        Task<bool> UpdateDiscountById(int id, DiscountDto discountDto);
        Task<bool> DeleteDiscountById(int id);
    }
}
