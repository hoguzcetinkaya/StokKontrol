using Abstraction.Entities;

namespace StokKontrol.Data
{
    public class Order : IOrder
    {
        public      int                     Id            { get; set; }
        public      string                  Name          { get; set; } = string.Empty;
        public      string                  Description   { get; set; } = string.Empty;
        public      int                     Price         { get; set; }
        public      int                     Quantity      { get; set; }

        public      int                     CustomerId    { get; set; }

        public      List<int>?              ProductIds   { get; set; }
        //Navigation
        public      Customer?               Customer      { get; set; }
    }
}
    