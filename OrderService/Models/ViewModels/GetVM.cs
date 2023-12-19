namespace OrderService.Models.ViewModels
{
    public class GetVM
    {
        public      string                  Name            { get; set; } = string.Empty;
        public      string                  Description     { get; set; } = string.Empty;
        public      int                     Price           { get; set; }
        public      int                     Quantity        { get; set; }
        public      string                  CustomerName    { get; set; } = string.Empty;
        public      string                  ProductsName    { get; set; }
    }
}
