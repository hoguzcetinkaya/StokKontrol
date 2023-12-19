﻿using Microsoft.EntityFrameworkCore;
using StokKontrol.Data;
using SupplierService.Converters;
using SupplierService.Extensions;
using SupplierService.Models.DTOs;

namespace SupplierService.Services
{
    public class SupplierProvider(StokDbContext dbContext) : ISupplierService
    {
        public async Task<Supplier> GetAsync(int id)
        {
            Supplier? supplier = await dbContext.Suppliers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id)!;

            if (supplier is null) throw new Exception("Supplier Not Found");
            return supplier;
        }
        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            List<Supplier> suppliers = await dbContext.Suppliers.AsNoTracking().ToListAsync();
            if (suppliers is null) throw new Exception("Suppliers is empty");
            return suppliers;

        }
        public async Task<Supplier> CreateAsync(CreateSupplierDTO createSupplierDTO)
        {
            Supplier supplier = Converter.GetInstance().Convert(createSupplierDTO);

            await dbContext.Suppliers.AddAsync(supplier);
            await dbContext.SaveChangesAsync();
            return supplier;
        }

        public async Task<IEnumerable<Supplier>> CreateManyAsync(IEnumerable<CreateSupplierDTO> createSupplierDTOs)
        {
            IEnumerable<Supplier> suppliers = Converter.GetInstance().Convert(createSupplierDTOs);

            await dbContext.AddRangeAsync(suppliers);
            await dbContext.SaveChangesAsync();

            return suppliers;
        }
        public async Task<Supplier> UpdateAsync(UpdateSupplierDTO updateSuplierDTO)
        {
            Supplier? supplier = await dbContext.Suppliers.FirstOrDefaultAsync(x => x.Id == updateSuplierDTO.Id);
            if(supplier is null) { throw new Exception(nameof(updateSuplierDTO)); }
            var updatedSupplier = Converter.GetInstance().Convert(updateSuplierDTO, supplier);

            dbContext.Suppliers.Update(updatedSupplier);
            await dbContext.SaveChangesAsync();
            return updatedSupplier;
        }

        public async Task<IEnumerable<Supplier>> UpdateManyAsync(IEnumerable<UpdateSupplierDTO> updateSuplierDTOs)
        {
            IEnumerable<Supplier>? suppliers = await dbContext.Suppliers.ToListAsync();
            if(!suppliers.Any()) throw new Exception("Güncellenecek müşteri bulunamadı.");

            IEnumerable<Supplier> updatedSuppliers = Converter.GetInstance().Convert(updateSuplierDTOs, suppliers);

            dbContext.Suppliers.UpdateRange(updatedSuppliers);
            await dbContext.SaveChangesAsync();
            return updatedSuppliers;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            Supplier? supplier = await dbContext.Suppliers.FirstOrDefaultAsync(x => x.Id == id);

            if (supplier is null) return false;

            dbContext.Suppliers.Remove(supplier);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteManyAsync(IEnumerable<int> ids)
        {
            List<Supplier> suppliers = await dbContext.Suppliers.Where(x => ids.Contains(x.Id)).AsNoTracking().ToListAsync();
            if (!suppliers.Any()) return false;
            dbContext.Suppliers.RemoveRange(suppliers);
            await dbContext.SaveChangesAsync();
            return true;
        }

        

        
    }
}
