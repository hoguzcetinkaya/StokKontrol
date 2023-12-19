using CategoryService.Models.DTOs;
using CategoryService.Services;
using Microsoft.AspNetCore.Mvc;
using StokKontrol.Data;

namespace CategoryService.Controllers
{
    [ApiController]
    [Route("controller")]
    public class CategoryController(ICategoryService categoryService) : Controller
    {
        private readonly ICategoryService categoryService = categoryService;

        [HttpGet("GetAll")]
        public async Task<IEnumerable<Category>> GetAll()
        {
            return await categoryService.GetAllAsync();
        }
        [HttpGet("Get")]
        public async Task<Category> Get(int id)
        {
            return await categoryService.GetAsync(id);
        }
        [HttpPost("Create")]
        public async Task<Category> Create(CreateCategoryDTO createCategoryDTO)
        {
            if (!ModelState.IsValid)
                throw new NullReferenceException("Model valid değil");

            return await categoryService.CreateAsync(createCategoryDTO);
        }

        [HttpPost("CreateMany")]
        public async Task<IEnumerable<Category>> CreateMany(IEnumerable<CreateCategoryDTO> createCategoryDTOs)
        {
            if (!ModelState.IsValid)
                throw new NullReferenceException("Model valid değil");

            return await categoryService.CreateManyAsync(createCategoryDTOs);
        }

        [HttpPut("Update")]
        public async Task<Category> Update(UpdateCategoryDTO updateCategoryDTO)
        {
            if (!ModelState.IsValid)
                throw new NullReferenceException("Model valid değil");

            return await categoryService.UpdateAsync(updateCategoryDTO);
        }
        [HttpPut("UpdateMany")]
        public async Task<IEnumerable<Category>> UpdateMany(IEnumerable<UpdateCategoryDTO> updateCategoryDTOs)
        {
            if (!ModelState.IsValid)
                throw new NullReferenceException("Model valid değil");

            return await categoryService.UpdateManyAsync(updateCategoryDTOs);
        }
        [HttpDelete("Delete")]
        public async Task<bool> Delete(int id)
        {
            return await categoryService.DeleteAsync(id);
        }
        [HttpDelete("DeleteMany")]
        public async Task<bool> DeleteMany(IEnumerable<int> ids)
        {
            return await categoryService.DeleteManyAsync(ids);
        }
    }
}
