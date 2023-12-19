using Microsoft.AspNetCore.Mvc;
using StokKontrol.Data;
using SupplierService.Models.DTOs;
using SupplierService.Services;

namespace SupplierService.Controllers
{
    public class SupplierController(ISupplierService supplierService) : Controller, ISupplierService
    {
        [HttpGet(nameof(GetAsync))]
        public async Task<Supplier> GetAsync(int id)
        {
            Supplier? supplier = await supplierService.GetAsync(id);

            if (supplier is null) return default(Supplier);

            return supplier;
        }
        [HttpGet(nameof(GetAllAsync))]
        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            IEnumerable<Supplier>? suppliers = await supplierService.GetAllAsync();

            if(suppliers is null) { return Enumerable.Empty<Supplier>(); }

            return suppliers;
        }

        [HttpPost(nameof(CreateAsync))]
        public async Task<Supplier> CreateAsync(CreateSupplierDTO createSupplierDTO)
        {
            if (!ModelState.IsValid) throw new NullReferenceException();

            return await supplierService.CreateAsync(createSupplierDTO);

        }
        [HttpPost(nameof(CreateManyAsync))]

        public async Task<IEnumerable<Supplier>> CreateManyAsync([FromBody]IEnumerable<CreateSupplierDTO> createSupplierDTOs)
        {
            if (!ModelState.IsValid) throw new NullReferenceException(ModelState.ErrorCount.ToString());

            return await supplierService.CreateManyAsync(createSupplierDTOs);
        }

        [HttpPut(nameof(UpdateAsync))]

        public Task<Supplier> UpdateAsync(UpdateSupplierDTO updateSuplierDTO)
        {
            if (!ModelState.IsValid) throw new NullReferenceException(ModelState.ErrorCount.ToString());

            return supplierService.UpdateAsync(updateSuplierDTO);
        }
        [HttpPut(nameof(UpdateManyAsync))]
        public Task<IEnumerable<Supplier>> UpdateManyAsync([FromBody]IEnumerable<UpdateSupplierDTO> updateSuplierDTOs)
        {
            if (!ModelState.IsValid) throw new NullReferenceException(ModelState.ErrorCount.ToString());

            return supplierService.UpdateManyAsync(updateSuplierDTOs);
        }


        [HttpDelete(nameof(DeleteAsync))]
        public async Task<bool> DeleteAsync(int id)
        {
            bool result = await supplierService.DeleteAsync(id);

            return result ? true : false;
        }


        [HttpDelete(nameof(DeleteManyAsync))]
        public async Task<bool> DeleteManyAsync([FromBody]IEnumerable<int> ids)
        {
            bool result = await supplierService.DeleteManyAsync(ids);

            return result ? true : false;
        }




    }
}
