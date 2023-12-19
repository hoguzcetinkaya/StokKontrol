using OrderService.Models.DTOs;
using StokKontrol.Data;
using System.Xml.Linq;

namespace OrderService.Converters
{
    public class Converter
    {
        private static readonly Lazy<Converter> lazy_singleInstance = new Lazy<Converter>(() => new Converter(), true);

        private                 /*Constructor*/ Converter()
        {
        }
        public static Converter GetInstance()
        {
            return lazy_singleInstance.Value;
        }

        public Order Convert(CreateOrderDTO createOrderDTO)
        {
            return new Order
            {
                Name = createOrderDTO.Name,
                Description = createOrderDTO.Description,
                Price = createOrderDTO.Price,
                Quantity = createOrderDTO.Quantity,
                CustomerId = createOrderDTO.CustomerId,
                ProductIds = createOrderDTO.ProductId
            };
        }

        public IEnumerable<Order> Convert(IEnumerable<CreateOrderDTO> createOrderDTOs)
        {
            List<Order> orders = new List<Order>();
            foreach (var item in createOrderDTOs)
            {
                orders.Add(new Order
                {
                    Name         = item.Name,
                    Description  = item.Description,
                    Price        = item.Price,
                    Quantity     = item.Quantity,
                    CustomerId   = item.CustomerId,
                    ProductIds   = item.ProductId
                });
            }
            return orders;
        }


        public Order Convert(UpdateOrderDTO updateOrderDTO, Order baseOrder)
        {
            baseOrder.Name         = updateOrderDTO.Name;
            baseOrder.Description  = updateOrderDTO.Description;
            baseOrder.Price        = updateOrderDTO.Price;
            baseOrder.Quantity     = updateOrderDTO.Quantity;
            baseOrder.ProductIds   = updateOrderDTO.ProductId;

            return baseOrder;
        }


    }
}
