using StokKontrol.Data;
using SupplierService.Models.DTOs;

namespace SupplierService.Services
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetAll        ();
        Task<Supplier>              Get           (int id);
        Task<Supplier>              Create        (CreateSupplierDTO createSupplierDTO); 
        Task<IEnumerable<Supplier>> CreateMany    (IEnumerable<CreateSupplierDTO> createSupplierDTOs);
        Task<Supplier>              Update        (UpdateSupplierDTO updateSuplierDTO);
        Task<IEnumerable<Supplier>> UpdateMany    (IEnumerable<UpdateSupplierDTO> updateSuplierDTOs);
        Task<bool>                  Delete        (int id);
        Task<bool>                  DeleteMany    (IEnumerable<int> ids);
    }
}
