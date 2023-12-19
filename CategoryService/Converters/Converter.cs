using CategoryService.Models.DTOs;
using StokKontrol.Data;

namespace CategoryService.Converters
{
    public class Converter
    {
        private static readonly Lazy<Converter> lazy_singleInstance = new Lazy<Converter>(() => new Converter(), true);

        private                 /*Constructor*/ Converter()
        {
        }
        public static Converter GetInstance()
        {
            return lazy_singleInstance.Value;
        }

        public Category Convert(CreateCategoryDTO createCategoryDTO)
        {
            return new Category
            {
                Name = createCategoryDTO.Name,
            };
        }
        public IEnumerable<Category> Convert(IEnumerable<CreateCategoryDTO> createCategoryDTOs)
        {
            List<Category> categories = new List<Category>();
            foreach (var item in createCategoryDTOs)
                categories.Add(new Category() { Name = item.Name });

            return categories;
        }

        public Category Convert(UpdateCategoryDTO updateCategoryDTO, Category baseCategory)
        {
            baseCategory.Name = updateCategoryDTO.Name;
            return baseCategory;
        }

        public IEnumerable<Category> Convert(IEnumerable<UpdateCategoryDTO> updateCategoryDTOs, IEnumerable<Category> baseCategories)
        {
            var categories = baseCategories.ToDictionary(c => c.Id, c => c);

            foreach (var item in updateCategoryDTOs)
            {
                if(categories.TryGetValue(item.Id, out var category))
                {
                    category.Name = item.Name;
                }
            }

            return categories.Values;
        }
    }
}
