using System.Diagnostics.Contracts;

namespace Service_Api.DTOs
{
    public class IngredientDto
    {
        public string Name { get; set; }
        public decimal IngredientPrice { get; set; }
        public string ImageName { get; set; }
        public int Id { get; set; }

        //Empty constructor
        public IngredientDto() { }
        
    }
}
