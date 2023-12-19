namespace Abstraction.Entities
{
    public interface ISupplier : IBaseEntity // TEDARİKÇİ
    {
        string      PhoneNumber { get; set; }
        string      Address     { get; set; }
        string      City        { get; set; }
        string      Country     { get; set; }

    }
}
