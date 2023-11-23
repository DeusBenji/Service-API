namespace Service_Api.DTOs
{
    public class OrderlineGroupDto
    {
        public int ProductId { get; set; }
        public int OrderlineId { get; set; }
        public int ComboId { get; set; }
        //Empty constructor
        public OrderlineGroupDto() { }
    }
}
