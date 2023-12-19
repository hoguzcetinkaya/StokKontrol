using System.ComponentModel.DataAnnotations;

namespace ProductService.Models.DTOs
{
    public class CreateProductDTO
    {
        [Required]
        public      string                  Name               { get; set; } = string.Empty;
        public      string                  Description        { get; set; } = string.Empty;
        public      int                     Price              { get; set; }
        public      int                     StockQuantity      { get; set; }
        public      int                     CategoryId         { get; set; }
        public      int                     SupplierId         { get; set; }
    }
}
