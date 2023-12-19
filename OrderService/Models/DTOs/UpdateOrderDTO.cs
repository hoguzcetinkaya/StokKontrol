namespace OrderService.Models.DTOs
{
    public class UpdateOrderDTO
    {
        public      int                     Id            { get; set; }
        public      string                  Name          { get; set; } = string.Empty;
        public      string                  Description   { get; set; } = string.Empty;
        public      int                     Price         { get; set; }
        public      int                     Quantity      { get; set; }
        public      List<int>?              ProductId     { get; set; }
    }
}
