namespace ProductService.Models.DTOs
{
    public class UpdateProductDTO
    {
        public int      Id                  { get; set; }
        public string   Name                { get; set; } = string.Empty;
        public string   Description         { get; set; } = string.Empty;
        public int      Price               { get; set; }
        public int      StockQuantity       { get; set; }
    }
}
