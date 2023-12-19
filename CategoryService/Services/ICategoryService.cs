using CategoryService.Models.DTOs;
using StokKontrol.Data;

namespace CategoryService.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>>    GetAllAsync        ();
        Task<Category>                 GetAsync           (int id);
        Task<Category>                 CreateAsync        (CreateCategoryDTO createCategoryDTO); 
        Task<IEnumerable<Category>>    CreateManyAsync    (IEnumerable<CreateCategoryDTO> createCategoryDTOs);
        Task<Category>                 UpdateAsync        (UpdateCategoryDTO updateCategoryDTO);
        Task<IEnumerable<Category>>    UpdateManyAsync    (IEnumerable<UpdateCategoryDTO> updateCategoryDTOs);
        Task<bool>                     DeleteAsync        (int id);
        Task<bool>                     DeleteManyAsync    (IEnumerable<int> ids);
    }
}
