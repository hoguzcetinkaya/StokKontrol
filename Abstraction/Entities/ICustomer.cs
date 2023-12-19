namespace Abstraction.Entities
{
    public interface ICustomer : IBaseEntity // Müşteri
    {
        string      Email        { get; set; }
        string      PhoneNumber  { get; set; }
        string      Address      { get; set; }
        string      City         { get; set; }
        string      Country      { get; set; }

    }
}
