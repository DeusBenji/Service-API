using Service_Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service_Api.BusinessLogicLayer.Interfaces
{
    public interface IShopData
    {
        Task<ShopDto> GetShopById(int id);
        Task<List<ShopDto>> GetAllShops();
        Task<int> CreateShop(ShopDto shopDto);
        Task<bool> UpdateShopById(int id, ShopDto shopDto);
        Task<bool> DeleteShopById(int id);
    }
}
