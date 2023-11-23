namespace Service_Api.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? ProductNumber { get; set; }
        public string? Description { get; set; }
        public double? BasePrice { get; set; }
        public int? Barcode { get; set; }
        public string CategoryType { get; set; }
        public int? ProductGroup { get; set; }
        public string ImageName { get; set; }

        public ProductDto()
        {
        }
    }
}
