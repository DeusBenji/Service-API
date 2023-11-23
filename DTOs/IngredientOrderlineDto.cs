namespace Service_Api.DTOs
{
    public class IngredientOrderlineDto
    {
        public int IngredientId { get; set; }
        public int OrderlineId { get; set; }
        public int Delta { get; set; }


        public IngredientOrderlineDto() { } 

    }
}
