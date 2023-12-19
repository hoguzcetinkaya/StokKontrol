using StokKontrol.Data;
using SupplierService.Models.DTOs;

namespace SupplierService.Services
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetAllAsync        ();
        Task<Supplier>              GetAsync           (int id);
        Task<Supplier>              CreateAsync        (CreateSupplierDTO createSupplierDTO); 
        Task<IEnumerable<Supplier>> CreateManyAsync    (IEnumerable<CreateSupplierDTO> createSupplierDTOs);
        Task<Supplier>              UpdateAsync        (UpdateSupplierDTO updateSuplierDTO);
        Task<IEnumerable<Supplier>> UpdateManyAsync    (IEnumerable<UpdateSupplierDTO> updateSuplierDTOs);
        Task<bool>                  DeleteAsync        (int id);
        Task<bool>                  DeleteManyAsync    (IEnumerable<int> ids);
    }
}
