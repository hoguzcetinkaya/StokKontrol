using Microsoft.AspNetCore.Mvc;
using ProductService.Models.DTOs;
using ProductService.Models.ViewModels;
using ProductService.Services;
using StokKontrol.Data;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController(IProductService  productService) : Controller//, IProductService
    {
        [HttpGet(nameof(Get))]
        public Task<GetVM> Get(int id)
        {
            return productService.GetAsync(id);
        }
        [HttpGet(nameof(GetAll))]
        public Task<IEnumerable<GetVM>> GetAll()
        {
            return productService.GetAllAsync();
        }
        [HttpPost(nameof(Create))]
        public Task<GetVM> Create(CreateProductDTO createProductDTO)
        {
            if (!ModelState.IsValid) throw new Exception("doğru giriniz");

            return productService.CreateAsync(createProductDTO);
        }
        [HttpPost(nameof(CreateMany))]
        public Task<IEnumerable<Product>> CreateMany(IEnumerable<CreateProductDTO> createProductDTOs)
        {
            if (!ModelState.IsValid) throw new Exception("doğru giriniz");

            return productService.CreateManyAsync(createProductDTOs);
        }
        [HttpDelete("Delete/{id}")]
        public Task<bool> Delete(int id)
        {
            return productService.DeleteAsync(id);
        }

        [HttpDelete(nameof(DeleteMany))]
        public Task<bool> DeleteMany(IEnumerable<int> ids)
        {
            return productService.DeleteManyAsync(ids);
        }



        [HttpPut(nameof(Update))]
        public async Task<GetVM> Update(UpdateProductDTO updateProductDTO)
        {
            return await productService.UpdateAsync(updateProductDTO);
        }
        [HttpPut(nameof(UpdateMany))]
        public async Task<IEnumerable<GetVM>> UpdateMany(IEnumerable<UpdateProductDTO> updateProductDTOs)
        {
            return await productService.UpdateManyAsync(updateProductDTOs);

        }
    }
}
