using AutoMapper;
using Service_Api.BusinessLogicLayer.Interfaces;
using Service_Api.DTOs;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service_Api.BusinessLogicLayer
{
    public class ProductGroupDataControl : IProductGroupData
    {
        private readonly IProductGroup _productGroupDatabaseAccess;
        private readonly IMapper _mapper;

        public ProductGroupDataControl(IProductGroup productGroupDatabaseAccess, IMapper mapper)
        {
            _productGroupDatabaseAccess = productGroupDatabaseAccess;
            _mapper = mapper;
        }

        public async Task<ProductGroupDto> GetProductGroupById(int id)
        {
            var productGroup = await _productGroupDatabaseAccess.GetProductGroupById(id);
            return _mapper.Map<ProductGroupDto>(productGroup);
        }

        public async Task<List<ProductGroupDto>> GetAllProductGroups()
        {
            var productGroups = await _productGroupDatabaseAccess.GetAllProductGroups();
            return _mapper.Map<List<ProductGroupDto>>(productGroups);
        }

        public async Task<int> CreateProductGroup(ProductGroupDto productGroupDto)
        {
            var productGroup = _mapper.Map<ProductGroup>(productGroupDto);
            return await _productGroupDatabaseAccess.CreateProductGroup(productGroup);
        }

        public async Task<bool> UpdateProductGroupById(int id, ProductGroupDto productGroupDto)
        {
            var productGroup = _mapper.Map<ProductGroup>(productGroupDto);
            productGroup.Id = id; // Set the ID for the update
            return await _productGroupDatabaseAccess.UpdateProductGroupById(productGroup);
        }

        public async Task<bool> DeleteProductGroupById(int id)
        {
            return await _productGroupDatabaseAccess.DeleteProductGroupById(id);
        }
    }
}
