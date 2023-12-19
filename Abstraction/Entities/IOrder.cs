namespace Abstraction.Entities
{
    public interface IOrder : IBaseEntity// Sipariş
    {
        string          Description  { get; set; }
        int             Price        { get; set; }
        int             Quantity     { get; set; }
        //int CustomerID { get; set; }
        //int ProductID { get; set; }
    }
}
