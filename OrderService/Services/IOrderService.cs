using OrderService.Models.DTOs;
using OrderService.Models.ViewModels;
using StokKontrol.Data;

namespace OrderService.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<GetVM>>    GetAllAsync        ();
        Task<GetVM>                 GetAsync           (int id);
        Task<Order>                 CreateAsync        (CreateOrderDTO createOrderDTO); 
        Task<IEnumerable<Order>>    CreateManyAsync    (IEnumerable<CreateOrderDTO> createOrderDTOs);
        Task<Order>                 UpdateAsync        (UpdateOrderDTO updateOrderDTO);
        Task<IEnumerable<Order>>    UpdateManyAsync    (IEnumerable<UpdateOrderDTO> updateOrderDTOs);
        Task                        DeleteAsync        (int id);
        Task                        DeleteManyAsync    (IEnumerable<int> ids);
    }
}
