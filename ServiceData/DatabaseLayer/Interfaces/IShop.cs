using ServiceData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer.Interfaces
{
    public interface IShop
    {
        Task<Shop> GetShopById(int id);
        Task<List<Shop>> GetAllShops();
        Task<int> CreateShop(Shop anShop);
        Task<bool> DeleteShopById(int id);
        Task<bool> UpdateShopById(Shop ShopToUpdate);
    }
}
