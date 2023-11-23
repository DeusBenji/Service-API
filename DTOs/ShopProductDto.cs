namespace Service_Api.DTOs
{
    public class ShopProductDto
    {
        public int ShopId { get; set; }
        public int ProductId { get; set; }

        //Empty constructor
        public ShopProductDto()
        {

        }

        //Constructor with parameters
        public ShopProductDto(int shopId, int productId)
        {
            ShopId = shopId;
            ProductId = productId;
        }
    }
}
