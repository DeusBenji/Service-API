namespace Service_Api.DTOs
{
    public class IngredientProductDto
    {
        public int ProductId { get; set; }
        public int IngredientId { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int Count { get; set; }
        //Empty constructor
        public IngredientProductDto() { }
        //Constructor with parameters
        public IngredientProductDto(int productId, int ingredientId, int min, int max, int count)
        {
            ProductId = productId;
            IngredientId = ingredientId;
            Min = min;
            Max = max;
            Count = count;
        }
    }
}
