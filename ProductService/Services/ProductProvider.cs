﻿using Abstraction.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Converters;
using ProductService.Extensions;
using ProductService.Models.DTOs;
using ProductService.Models.ViewModels;
using StokKontrol.Data;

namespace ProductService.Services
{
    public class ProductProvider(StokDbContext dbContext) : IProductService
    {
        public async Task<GetVM> GetAsync(int id)
        {
            var product = await dbContext.Products.AsNoTracking()
                .Where(x => x.Id == id) // FirstOrDefault yerine Where kullanmamızın sebebi. Select kullanmak için yapının IQueryable türünde olması gerekliymiş.
                                        // Select için db'ye gidip gelmemek gerekliymiş. Db'ye gitmeden kullanılır ki db'den çekilecek dataları ona göre ayarlasın
                .Select(x => new GetVM
                {
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    StockQuantity = x.StockQuantity,
                    CategoryName = x.Category.Name,
                    SupplierName = x.Supplier.Name,

                }).FirstOrDefaultAsync();


            if (product == null) throw new Exception($"Product not found. Id : {id}");

            return product;
        }
        public async Task<IEnumerable<GetVM>> GetAllAsync()
        {
            IEnumerable<GetVM> products = await dbContext.Products.AsNoTracking()
                .Select(x => new GetVM
                {
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    StockQuantity = x.StockQuantity,
                    CategoryName = x.Category.Name,
                    SupplierName = x.Supplier.Name,

                }).ToListAsync();

            return products;
        }

        public async Task<Product> CreateAsync(CreateProductDTO createProductDTO)
        {
            Product product = Converter.GetInstance().Convert(createProductDTO);
            bool isCategoryValid = await dbContext.Categories.AnyAsync(c => c.Id == product.CategoryId);
            bool isSupplierValid = await dbContext.Suppliers.AnyAsync(s => s.Id == product.SupplierId);
            await dbContext.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> CreateManyAsync([FromBody] IEnumerable<CreateProductDTO> createProductDTOs)
        {
            IEnumerable<Product> products = Converter.GetInstance().Convert(createProductDTOs);

            if (!products.Any()) throw new Exception("Products not found");

            await dbContext.AddRangeAsync(products);
            await dbContext.SaveChangesAsync();
            return products;
        }

        public async Task<GetVM> UpdateAsync(UpdateProductDTO updateProductDTO)
        {
            Product? product = await dbContext.Products
                .Include(x=> x.Category).Include(x=> x.Supplier).FirstOrDefaultAsync(x=> x.Id== updateProductDTO.Id);
            if (product == null) throw new Exception($"{updateProductDTO.Name} not found");

            Product newProduct = Converter.GetInstance().Convert(updateProductDTO, product);

            GetVM getVM = new GetVM
            {
                Name           = newProduct.Name         ,
                Price          = newProduct.Price        ,
                Description    = newProduct.Description  ,
                StockQuantity  = newProduct.StockQuantity,
                CategoryName   = newProduct.Category.Name,
                SupplierName   = newProduct.Supplier.Name,
            };

            dbContext.Products.Update(newProduct);
            await dbContext.SaveChangesAsync();
            return getVM;
        }

        public async Task<IEnumerable<GetVM>> UpdateManyAsync([FromBody]IEnumerable<UpdateProductDTO> updateProductDTOs)
        {
            IEnumerable<Product> products = await dbContext.Products.Include(x => x.Category).Include(x => x.Supplier).ToListAsync();
            if (!products.Any()) throw new Exception($"Products not found");

            IEnumerable<Product> newProducts = Converter.GetInstance().Convert(updateProductDTOs,products);

            dbContext.UpdateRange(newProducts);
            await dbContext.SaveChangesAsync();

            return newProducts.Select(x=> new GetVM
            {
                Name           = x.Name         ,
                Description    = x.Description  ,
                Price          = x.Price        ,
                StockQuantity  = x.StockQuantity,
                CategoryName   = x.Category.Name,
                SupplierName   = x.Supplier.Name,
            }).ToList();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry x = dbContext.Remove(id);
            if(x.State== EntityState.Deleted)
            {
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteManyAsync([FromBody] IEnumerable<int> ids)
        {
            List<Product> products= await dbContext.Products.Where(x => ids.Contains(x.Id)).AsNoTracking().ToListAsync();
            if (!products.Any()) return false;
            dbContext.Products.RemoveRange(products);
            await dbContext.SaveChangesAsync();

            return true;
        }




    }
}
