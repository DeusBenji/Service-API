using AutoMapper;
using Service_Api.BusinessLogicLayer.Interfaces;
using Service_Api.DTOs;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service_Api.BusinessLogicLayer
{
    public class ProductDataControl : IProductData
    {
        private readonly IProduct _productDatabaseAccess;
        private readonly IMapper _mapper;

        public ProductDataControl(IProduct productDatabaseAccess, IMapper mapper)
        {
            _productDatabaseAccess = productDatabaseAccess;
            _mapper = mapper;
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await _productDatabaseAccess.GetProductById(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<List<ProductDto>> GetAllProducts()
        {
            var products = await _productDatabaseAccess.GetAllProducts();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<int> CreateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            return await _productDatabaseAccess.CreateProduct(product);
        }

        public async Task<bool> UpdateProductById(int id, ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            product.Id = id; // Set the ID for the update
            return await _productDatabaseAccess.UpdateProductById(product);
        }

        public async Task<bool> DeleteProductById(int id)
        {
            return await _productDatabaseAccess.DeleteProductById(id);
        }
        public async Task<List<ProductDto>> GetProductsByShopId(int shopId)
        {
            var products = await _productDatabaseAccess.GetProductsByShopId(shopId);
            return _mapper.Map<List<ProductDto>>(products);
        }


    }
}
