using CategoryService.Converters;
using CategoryService.Extensions;
using CategoryService.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using StokKontrol.Data;

namespace CategoryService.Services
{
    public class CategoryProvider(StokDbContext dbContext) : ICategoryService
    {
        private readonly StokDbContext dbContext = dbContext;
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            List<Category> categories = await dbContext.Categories.AsNoTracking().ToListAsync();
            return categories;
        }
        public async Task<Category> GetAsync(int id)
        {
            Task<Category> category = dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id)!;

            if (category is null) throw new Exception("Category Not Found");
            return await category;
        }
        public async Task<Category> CreateAsync(CreateCategoryDTO createCategoryDTO)
        {
            Category category = Converter.GetInstance().Convert(createCategoryDTO);
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }
        public async Task<IEnumerable<Category>> CreateManyAsync(IEnumerable<CreateCategoryDTO> createCategoryDTOs)
        {
            IEnumerable<Category> categories = Converter.GetInstance().Convert(createCategoryDTOs);
            await dbContext.AddRangeAsync(categories);
            await dbContext.SaveChangesAsync();
            return categories;
        }
        public async Task<Category> UpdateAsync(UpdateCategoryDTO updateCategoryDTO)
        {
            var category = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == updateCategoryDTO.Id);
            Category updatedCategory = Converter.GetInstance().Convert(updateCategoryDTO, category);
            dbContext.Categories.Update(updatedCategory);
            await dbContext.SaveChangesAsync();
            return updatedCategory;

        }

        public async Task<IEnumerable<Category>> UpdateManyAsync(IEnumerable<UpdateCategoryDTO> updateCategoryDTOs)
        {
            var updateCategoryIds = updateCategoryDTOs.Select(x => x.Id); // güncellenecek id'ler
            var updateCategory = await dbContext.Categories.Where(x => updateCategoryIds.Contains(x.Id)).ToListAsync(); // güncellenecek id'ler db'de eşleşiyor mu ?
            IEnumerable<Category> category = Converter.GetInstance().Convert(updateCategoryDTOs,updateCategory);
            dbContext.Categories.UpdateRange(category);
            await dbContext.SaveChangesAsync();
            return category;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            Category? category = await dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteManyAsync(IEnumerable<int> ids)
        {
            // IDs ile eşleşen tüm kategorileri bul
            List<Category> categories = dbContext.Categories.Where(category => ids.Contains(category.Id)).AsNoTracking().ToList();
            if (!categories.Any())
                return false;
            dbContext.Categories.RemoveRange(categories);
            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
