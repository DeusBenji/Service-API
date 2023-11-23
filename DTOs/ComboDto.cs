namespace Service_Api.DTOs
{
    public class ComboDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }

        public decimal ComboPrice { get; set; }

        public ComboDto() { }
    }
}
