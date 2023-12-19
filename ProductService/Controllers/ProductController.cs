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
        [HttpGet(nameof(GetAsync))]
        public Task<GetVM> GetAsync(int id)
        {
            return productService.GetAsync(id);
        }
        [HttpGet(nameof(GetAllAsync))]
        public Task<IEnumerable<GetVM>> GetAllAsync()
        {
            return productService.GetAllAsync();
        }
        [HttpPost(nameof(CreateAsync))]
        public Task<Product> CreateAsync(CreateProductDTO createProductDTO)
        {
            if (!ModelState.IsValid) throw new Exception("doğru giriniz");

            return productService.CreateAsync(createProductDTO);
        }
        [HttpPost(nameof(CreateManyAsync))]
        public Task<IEnumerable<Product>> CreateManyAsync(IEnumerable<CreateProductDTO> createProductDTOs)
        {
            if (!ModelState.IsValid) throw new Exception("doğru giriniz");

            return productService.CreateManyAsync(createProductDTOs);
        }
        [HttpDelete(nameof(DeleteAsync))]
        public Task<bool> DeleteAsync(int id)
        {
            return productService.DeleteAsync(id);
        }

        [HttpDelete(nameof(DeleteManyAsync))]
        public Task<bool> DeleteManyAsync(IEnumerable<int> ids)
        {
            return productService.DeleteManyAsync(ids);
        }



        [HttpPut(nameof(UpdateAsync))]
        public async Task<GetVM> UpdateAsync(UpdateProductDTO updateProductDTO)
        {
            return await productService.UpdateAsync(updateProductDTO);
        }
        [HttpPut(nameof(UpdateManyAsync))]
        public async Task<IEnumerable<GetVM>> UpdateManyAsync(IEnumerable<UpdateProductDTO> updateProductDTOs)
        {
            return await productService.UpdateManyAsync(updateProductDTOs);

        }
    }
}
