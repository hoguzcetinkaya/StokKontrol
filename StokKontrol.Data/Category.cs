using Abstraction.Entities;

namespace StokKontrol.Data
{
    public class Category : ICategory
    {
        public      int         Id    { get; set; }
        public      string      Name  { get; set; } = string.Empty;


        public ICollection<Product> Products { get; set; }
    }
}
