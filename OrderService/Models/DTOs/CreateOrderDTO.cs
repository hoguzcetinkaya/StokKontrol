namespace OrderService.Models.DTOs
{
    public class CreateOrderDTO
    {
        public      string                  Name          { get; set; } = string.Empty;
        public      string                  Description   { get; set; } = string.Empty;
        public      int                     Price         { get; set; }
        public      int                     Quantity      { get; set; }
        public      int                     CustomerId    { get; set; }
        public      List<int>?              ProductId     { get; set; }
    }
}
