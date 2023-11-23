using AutoMapper;
using Service_Api.BusinessLogicLayer.Interfaces;
using Service_Api.DTOs;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service_Api.BusinessLogicLayer
{
    public class ShopDataControl : IShopData
    {
        private readonly IShop _shopDatabaseAccess;
        private readonly IMapper _mapper;

        public ShopDataControl(IShop shopDatabaseAccess, IMapper mapper)
        {
            _shopDatabaseAccess = shopDatabaseAccess;
            _mapper = mapper;
        }

        public async Task<ShopDto> GetShopById(int id)
        {
            var shop = await _shopDatabaseAccess.GetShopById(id);
            return _mapper.Map<ShopDto>(shop);
        }

        public async Task<List<ShopDto>> GetAllShops()
        {
            var shops = await _shopDatabaseAccess.GetAllShops();
            return _mapper.Map<List<ShopDto>>(shops);
        }

        public async Task<int> CreateShop(ShopDto shopDto)
        {
            var shop = _mapper.Map<Shop>(shopDto);
           
            return await _shopDatabaseAccess.CreateShop(shop);
        }

        public async Task<bool> UpdateShopById(int id, ShopDto shopDto)
        {
            var shop = _mapper.Map<Shop>(shopDto);
            shop.Id = id; // Set the ID for the update
            return await _shopDatabaseAccess.UpdateShopById(shop);
        }

        public async Task<bool> DeleteShopById(int id)
        {
            return await _shopDatabaseAccess.DeleteShopById(id);
        }
    }
}
