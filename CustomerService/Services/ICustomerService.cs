using CustomerService.Models.DTOs;
using StokKontrol.Data;

namespace CustomerService.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>>    GetAllAsync        ();
        Task<Customer>                 GetAsync           (int id);
        Task<Customer>                 CreateAsync        (CreateCustomerDTO createCustomerDTO); 
        Task<IEnumerable<Customer>>    CreateManyAsync    (IEnumerable<CreateCustomerDTO> createCustomerDTOs);
        Task<Customer>                 UpdateAsync        (UpdateCustomerDTO updateCustomerDTO);
        Task<IEnumerable<Customer>>    UpdateManyAsync    (IEnumerable<UpdateCustomerDTO> updateCustomerDTOs);
        Task<bool>                     DeleteAsync        (int id);
        Task<bool>                     DeleteManyAsync    (IEnumerable<int> ids);
    }
}
