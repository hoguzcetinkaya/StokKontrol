using CustomerService.Converters;
using CustomerService.Extensions;
using CustomerService.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using StokKontrol.Data;

namespace CustomerService.Services
{
    public class CustomerProvider : ICustomerService
    {
        private readonly StokDbContext dbContext;

        public CustomerProvider(StokDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await dbContext.Customers.AsNoTracking().ToListAsync();
        }
        public async Task<Customer> GetAsync(int id)
        {
            Customer? customer = await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (customer is null) throw new NullReferenceException("Customer yok");

            return customer;
        }

        public async Task<Customer> CreateAsync(CreateCustomerDTO createCustomerDTO)
        {
            Customer customer = Converter.GetInstance().Convert(createCustomerDTO);

            await dbContext.AddAsync(customer);
            await dbContext.SaveChangesAsync();
            return customer;
        }
        public async Task<IEnumerable<Customer>> CreateManyAsync(IEnumerable<CreateCustomerDTO> createCustomerDTOs)
        {
            IEnumerable<Customer> customers = Converter.GetInstance().Convert(createCustomerDTOs);

            await dbContext.AddRangeAsync(customers);
            await dbContext.SaveChangesAsync();
            return customers;
        }

        public async Task<Customer> UpdateAsync(UpdateCustomerDTO updateCustomerDTO)
        {
            Customer? customer = await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == updateCustomerDTO.Id);
            if(customer is null) { throw new NullReferenceException(nameof(updateCustomerDTO)); }

            customer = Converter.GetInstance().Convert(updateCustomerDTO,customer);
            dbContext.Update(customer);
            await dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<IEnumerable<Customer>> UpdateManyAsync(IEnumerable<UpdateCustomerDTO> updateCustomerDTOs)
        {
            var updateIds = updateCustomerDTOs.Select(dto => dto.Id).ToList();
            var customersToUpdate = dbContext.Customers.Where(c => updateIds.Contains(c.Id)).ToList();
            // Güncellenecek müşteri yoksa hata fırlat
            if (!customersToUpdate.Any())
            {
                throw new NullReferenceException("Güncellenecek müşteri bulunamadı.");
            }

            IEnumerable<Customer> newCustomer = Converter.GetInstance().Convert(updateCustomerDTOs, customersToUpdate);
            dbContext.UpdateRange(newCustomer);
            await dbContext.SaveChangesAsync();
            return newCustomer;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    var ordersToDelete = dbContext.Orders.Where(p => p.CustomerId == id);
                    dbContext.Orders.RemoveRange(ordersToDelete);
                    var customerToDelete = dbContext.Customers.Find(id);
                    dbContext.Customers.Remove(customerToDelete);

                    await dbContext.SaveChangesAsync();
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                    // Hata işleme
                }
            }
        }
        public async Task<bool> DeleteManyAsync(IEnumerable<int> ids)
        {
            List<Customer> customers = await dbContext.Customers.Where(x => ids.Contains(x.Id)).AsNoTracking().ToListAsync();
            dbContext.Customers.RemoveRange(customers);
            await dbContext.SaveChangesAsync();
            return true;

        }
    }
}
