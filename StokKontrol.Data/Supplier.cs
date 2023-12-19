using Abstraction.Entities;

namespace StokKontrol.Data
{
    public class Supplier : ISupplier
    {
        public      int         Id           { get; set; }
        public      string      Name         { get; set; } = string.Empty;
        public      string      PhoneNumber  { get; set; } = string.Empty;
        public      string      Address      { get; set; } = string.Empty;
        public      string      City         { get; set; } = string.Empty;
        public      string      Country      { get; set; } = string.Empty;




        public ICollection<Product> Products { get; set; }
    }
}
