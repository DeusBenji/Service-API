namespace Service_Api.DTOs
{
    public class DiscountDto
    {
        public int Id { get; set; }
        public decimal Rate { get; set; }
        public int ProductGroupId { get; set; }
        public int CustomerGroupId { get; set; }


        //Empty constructor
        public DiscountDto() { }


    }


}
