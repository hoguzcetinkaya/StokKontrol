namespace Abstraction.Entities
{
    public interface IProduct : IBaseEntity // ÜRÜN
    {
        string       Description      { get; set; }
        int          Price            { get; set; }
        int          StockQuantity    { get; set; }
        //int          SupplierID       { get; set; }
        //int          CategoryID       { get; set; }

    }
}
