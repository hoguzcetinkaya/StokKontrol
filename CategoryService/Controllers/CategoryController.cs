using CategoryService.Models.DTOs;
using CategoryService.Services;
using Microsoft.AspNetCore.Mvc;
using StokKontrol.Data;

namespace CategoryService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CategoryController(ICategoryService categoryService) : Controller
    {
        private readonly ICategoryService categoryService = categoryService;

        [HttpGet(nameof(GetAll))]
        public async Task<IEnumerable<Category>> GetAll()
        {
            return await categoryService.GetAllAsync();
        }
        [HttpGet(nameof(Get))]
        public async Task<Category> Get(int id)
        {
            return await categoryService.GetAsync(id);
        }
        [HttpPost(nameof(Create))]
        public async Task<Category> Create(CreateCategoryDTO createCategoryDTO)
        {
            if (!ModelState.IsValid)
                throw new NullReferenceException("Model valid değil");

            return await categoryService.CreateAsync(createCategoryDTO);
        }

        [HttpPost(nameof(CreateMany))]
        public async Task<IEnumerable<Category>> CreateMany(IEnumerable<CreateCategoryDTO> createCategoryDTOs)
        {
            if (!ModelState.IsValid)
                throw new NullReferenceException("Model valid değil");

            return await categoryService.CreateManyAsync(createCategoryDTOs);
        }

        [HttpPut(nameof(Update))]
        public async Task<Category> Update(UpdateCategoryDTO updateCategoryDTO)
        {
            if (!ModelState.IsValid)
                throw new NullReferenceException("Model valid değil");

            return await categoryService.UpdateAsync(updateCategoryDTO);
        }
        [HttpPut(nameof(UpdateMany))]
        public async Task<IEnumerable<Category>> UpdateMany(IEnumerable<UpdateCategoryDTO> updateCategoryDTOs)
        {
            if (!ModelState.IsValid)
                throw new NullReferenceException("Model valid değil");

            return await categoryService.UpdateManyAsync(updateCategoryDTOs);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<bool> Delete(int id)
        {
            return await categoryService.DeleteAsync(id);
        }
        [HttpDelete(nameof(DeleteMany))]
        public async Task<bool> DeleteMany(IEnumerable<int> ids)
        {
            return await categoryService.DeleteManyAsync(ids);
        }
    }
}
