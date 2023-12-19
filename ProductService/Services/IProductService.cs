using ProductService.Models.DTOs;
using ProductService.Models.ViewModels;
using StokKontrol.Data;

namespace ProductService.Services
{
    public interface IProductService
    {
        Task<IEnumerable<GetVM>>    GetAllAsync        ();
        Task<GetVM>                 GetAsync           (int id);
        Task<Product>               CreateAsync        (CreateProductDTO createProductDTO); 
        Task<IEnumerable<Product>>  CreateManyAsync    (IEnumerable<CreateProductDTO> createProductDTOs);
        Task<GetVM>               UpdateAsync        (UpdateProductDTO updateProductDTO);
        Task<IEnumerable<GetVM>>  UpdateManyAsync    (IEnumerable<UpdateProductDTO> updateProductDTOs);
        Task<bool>                  DeleteAsync        (int id);
        Task<bool>                  DeleteManyAsync    (IEnumerable<int> ids);
    }
}
