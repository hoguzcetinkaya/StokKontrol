using CustomerService.Models.DTOs;
using CustomerService.Services;
using Microsoft.AspNetCore.Mvc;
using StokKontrol.Data;

namespace CustomerService.Controllers
{
    [ApiController]
    [Route("controller")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await customerService.GetAllAsync();
        }

        [HttpGet(nameof(Get))]
        public async Task<Customer> Get(int id) 
        {
            return await customerService.GetAsync(id);
        }

        [HttpPost(nameof(Create))]
        public async Task<Customer> Create(CreateCustomerDTO createCustomerDTO)
        {
            if (!ModelState.IsValid) throw new NullReferenceException();

            return await customerService.CreateAsync(createCustomerDTO);
        }

        [HttpPost(nameof(CreateMany))]
        public async Task<IEnumerable<Customer>> CreateMany(IEnumerable<CreateCustomerDTO> createCustomerDTOs)
        {
            if (!ModelState.IsValid) throw new NullReferenceException();

            return await customerService.CreateManyAsync(createCustomerDTOs);
        }

        [HttpPut(nameof(Update))]
        public async Task<Customer> Update(UpdateCustomerDTO updateCustomerDTO)
        {
            if (!ModelState.IsValid) throw new NullReferenceException();

            return await customerService.UpdateAsync(updateCustomerDTO);
        }

        [HttpPut(nameof(UpdateMany))]
        public async Task<IEnumerable<Customer>> UpdateMany(IEnumerable<UpdateCustomerDTO> updateCustomerDTOs)
        {
            if (!ModelState.IsValid) throw new NullReferenceException();

            return await customerService.UpdateManyAsync(updateCustomerDTOs);
        }

        [HttpDelete(nameof(Delete))]
        public async Task<bool> Delete(int id)
        {
            return await customerService.DeleteAsync(id);
        }

        [HttpDelete(nameof(DeleteMany))]
        public async Task<bool> DeleteMany(IEnumerable<int> ids)
        {
            return await customerService.DeleteManyAsync(ids);
        }
    }
}
