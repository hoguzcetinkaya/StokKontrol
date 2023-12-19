using Microsoft.AspNetCore.Mvc;
using OrderService.Models.DTOs;
using OrderService.Models.ViewModels;
using OrderService.Services;
using StokKontrol.Data;

namespace OrderService.Controllers
{
    public class OrderController(IOrderService orderService) : Controller
    {
        [HttpGet(nameof(Get))]
        public async Task<GetVM> Get(int id)
        {
            return await  orderService.GetAsync(id);
        }
        [HttpGet(nameof(GetAll))]
        public async Task<IEnumerable<GetVM>> GetAll()
        {
            return await orderService.GetAllAsync();
        }

        [HttpPost(nameof(Create))]
        public async Task<Order> Create([FromBody]CreateOrderDTO createOrderDTO)
        {
            if (!ModelState.IsValid) throw new Exception("Model is not valid");

            return await orderService.CreateAsync(createOrderDTO);
        }
    }
}
