using AutoMapper;
using Service_Api.BusinessLogicLayer.Interfaces;
using Service_Api.DTOs;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service_Api.BusinessLogicLayer
{
    public class DiscountDataControl : IDiscountData
    {
        private readonly IDiscount _discountDatabaseAccess;
        private readonly IMapper _mapper;

        public DiscountDataControl(IDiscount discountDatabaseAccess, IMapper mapper)
        {
            _discountDatabaseAccess = discountDatabaseAccess;
            _mapper = mapper;
        }

        public async Task<DiscountDto> GetDiscountById(int id)
        {
            var discount = await _discountDatabaseAccess.GetDiscountById(id);
            return _mapper.Map<DiscountDto>(discount);
        }

        public async Task<List<DiscountDto>> GetAllDiscounts()
        {
            var discounts = await _discountDatabaseAccess.GetAllDiscount();
            return _mapper.Map<List<DiscountDto>>(discounts);
        }

        public async Task<int> CreateDiscount(DiscountDto discountDto)
        {
            var discount = _mapper.Map<Discount>(discountDto);
            return await _discountDatabaseAccess.CreateDiscount(discount);
        }

        public async Task<bool> UpdateDiscountById(int id, DiscountDto discountDto)
        {
            var discount = _mapper.Map<Discount>(discountDto);
            discount.Id = id; // Set the ID for the update
            return await _discountDatabaseAccess.UpdateDiscountById(discount);
        }

        public async Task<bool> DeleteDiscountById(int id)
        {
            return await _discountDatabaseAccess.DeleteDiscountById(id);
        }
    }
}
