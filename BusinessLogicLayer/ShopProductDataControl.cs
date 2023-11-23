using AutoMapper;
using Service_Api.BusinessLogicLayer.Interfaces;
using Service_Api.DTOs;
using ServiceData.DatabaseLayer;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service_Api.BusinessLogicLayer
{
    public class ShopProductDataControl : IShopProductData
    {
        private readonly IShopProduct _shopProductDatabaseAccess;
        private readonly IMapper _mapper;

        public ShopProductDataControl(IShopProduct shopProductDatabaseAccess, IMapper mapper)
        {
            _shopProductDatabaseAccess = shopProductDatabaseAccess;
            _mapper = mapper;
        }

        public async Task CreateShopProduct(ShopProductDto shopProductDto)
        {
            var shopProduct = _mapper.Map<ShopProduct>(shopProductDto);
            await _shopProductDatabaseAccess.CreateShopProduct(shopProduct);

        }

        public async Task<bool> DeleteShopProductByIds(int shopId, int productId)
        {
            return await _shopProductDatabaseAccess.DeleteShopProductByIds(shopId, productId);
        }

        public async Task<List<ShopProductDto>> GetAllShopProducts()
        {
            var shopProducts = await _shopProductDatabaseAccess.GetAllShopProducts();
            return _mapper.Map<List<ShopProductDto>>(shopProducts);
        }

        public async Task<List<ShopProductDto>> GetShopProductsByShopId(int shopId)
        {
            var shopProducts = await _shopProductDatabaseAccess.GetShopProductByShopId(shopId);
            return _mapper.Map<List<ShopProductDto>>(shopProducts);
        }

        public async Task<ShopProductDto> GetShopProductByIds(int shopId, int productId)
        {
            var shopProduct = await _shopProductDatabaseAccess.GetShopProductByIds(shopId, productId);
            return _mapper.Map<ShopProductDto>(shopProduct);
        }

        public async Task<bool> UpdateShopProductByIds(int shopId, int productId, ShopProductDto shopProductDto)
        {
            var shopProduct = _mapper.Map<ShopProduct>(shopProductDto);
            shopProduct.ShopId = shopId;
            shopProduct.ProductId = productId;
            return await _shopProductDatabaseAccess.UpdateShopProductByIds(shopProduct);
        }
    }
}
