namespace ProductService.Models.ViewModels
{
    public class GetVM
    {
        public      string                  Name          { get;set; } = string.Empty;
        public      string                  Description   { get;set; } = string.Empty;
        public      int                     Price         { get;set; }
        public      int                     StockQuantity { get;set; }
        public      string                  SupplierName  { get;set; } = string.Empty;
        public      string                  CategoryName  { get;set; } = string.Empty;
    }
}
