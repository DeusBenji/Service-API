using Service_Api.DTOs;
using AutoMapper;
using ServiceData.ModelLayer;

namespace Service_Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CustoemrGroup
            CreateMap<CustomerGroup, CustomerGroupDto>(); // Configure the mapping
            CreateMap<CustomerGroupDto, CustomerGroup>(); // Configure reverse mapping


            //Discount
            CreateMap<Discount, DiscountDto>(); // Configure the mapping
            CreateMap<DiscountDto, Discount>(); // Configure reverse mapping

            //Product
            CreateMap<Product, ProductDto>(); // Configure the mapping
            CreateMap<ProductDto, Product>(); // Configure reverse mapping

            //Shop
            CreateMap<Shop, ShopDto>(); // Configure the mapping
            CreateMap<ShopDto, Shop>(); // Configure reverse mapping

            //Ingredient
            CreateMap<Ingredient, IngredientDto>(); // Configure the mapping
            CreateMap<IngredientDto, Ingredient>(); // Configure reverse mapping

            //ProductGroup
            CreateMap<ProductGroup, ProductGroupDto>(); // Configure the mapping
            CreateMap<ProductGroupDto, ProductGroup>(); // Configure reverse mapping

            //Combo
            CreateMap<Combo, ComboDto>(); // Configure the mapping
            CreateMap<ComboDto, Combo>(); // Configure reverse mapping

            //ShopProduct
            CreateMap<ShopProduct, ShopProductDto>(); // Configure the mapping
            CreateMap<ShopProductDto, ShopProduct>(); // Configure reverse mapping

            //IngredientProduct
            CreateMap<IngredientProduct, IngredientProductDto>(); // Configure the mapping
            CreateMap<IngredientProductDto, IngredientProduct>(); // Configure reverse mapping

            //IngredientOrderline
            CreateMap<IngredientOrderline, IngredientOrderlineDto>(); // Configure the mapping
            CreateMap<IngredientOrderlineDto, IngredientOrderline>(); // Configure reverse mappin

            //OrderlineGroup
            CreateMap<OrderlineGroup, OrderlineGroupDto>(); // Configure the mapping
            CreateMap<OrderlineGroupDto, OrderlineGroup>(); // Configure reverse mapping

        }

    }
}
