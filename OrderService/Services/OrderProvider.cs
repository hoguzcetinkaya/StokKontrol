﻿using Microsoft.EntityFrameworkCore;
using OrderService.Converters;
using OrderService.Extensions;
using OrderService.Models.DTOs;
using OrderService.Models.ViewModels;
using StokKontrol.Data;

namespace OrderService.Services
{
    public class OrderProvider(StokDbContext dbContext) : IOrderService
    {
        public async Task<GetVM> GetAsync(int id)
        {

            Order? order = await dbContext.Orders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            //List<Product> products = await dbContext.Products.Where(x => ids.Contains(x.Id)).AsNoTracking().ToListAsync();
            var products = dbContext.Products.AsNoTracking().Where(x => order.ProductIds.Contains(x.Id)).Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            IEnumerable<string> yyy = products.Select(x => x.Name);

            string orderProducts = string.Join(',', yyy);



            if (order is null) throw new Exception($"{id} not found");

            return dbContext.Orders.AsNoTracking()
                .Select(x => new GetVM
                {
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    CustomerName = x.Customer!.Name,
                    ProductsName = orderProducts

                }).First();
        }
        public async Task<IEnumerable<GetVM>> GetAllAsync()
        {
            List<Product> products = await dbContext.Products.ToListAsync();

            //var orderProducts = orders.Join(products, o => o.ProductIds, p => p.Id, (o, p) => new GetVM
            //{ 
            //    Name = o.Name,
            //    Description = o.Description,
            //    Price = o.Price,
            //    Quantity = o.Quantity,
            //    CustomerName = o.Customer!.Name,
            //    ProductsName = p.Name

            //});
            // Önce tüm siparişleri ve ilişkili müşterileri hafızaya al
            List<Order> ordersWithCustomers = dbContext.Orders.Include(o => o.Customer).AsNoTracking().ToList();

            // Ardından ürün isimlerini hafızada işle
            var viewModelList = ordersWithCustomers.Select(x => new GetVM
            {
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Quantity = x.Quantity,
                CustomerName = x.Customer != null ? x.Customer.Name : string.Empty,
                ProductsName = x.ProductIds != null ? string.Join(',', products.Where(p => x.ProductIds.Contains(p.Id)).Select(p => p.Name)) : string.Empty
            }).ToList();

            return viewModelList;

        }

        public async Task<Order> CreateAsync(CreateOrderDTO createOrderDTO)
        {
            Order order = Converter.GetInstance().Convert(createOrderDTO);

            await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<Order>> CreateManyAsync(IEnumerable<CreateOrderDTO> createOrderDTOs)
        {
            IEnumerable<Order> orders = Converter.GetInstance().Convert(createOrderDTOs);

            await dbContext.Orders.AddRangeAsync(orders);
            await dbContext.SaveChangesAsync();
            return orders;
        }

        public Task<Order> UpdateAsync(UpdateOrderDTO updateOrderDTO)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> UpdateManyAsync(IEnumerable<UpdateOrderDTO> updateOrderDTOs)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteManyAsync(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }



    }
}
