using Microsoft.AspNetCore.Mvc;
using StokKontrol.Data;
using SupplierService.Models.DTOs;
using SupplierService.Services;

namespace SupplierService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class SupplierController(ISupplierService supplierService) : Controller, ISupplierService
    {
        [HttpGet(nameof(Get))]
        public async Task<Supplier> Get(int id)
        {
            Supplier? supplier = await supplierService.Get(id);

            if (supplier is null) return default(Supplier);

            return supplier;
        }
        [HttpGet(nameof(GetAll))]
        public async Task<IEnumerable<Supplier>> GetAll()
        {
            IEnumerable<Supplier>? suppliers = await supplierService.GetAll();

            if(suppliers is null) { return Enumerable.Empty<Supplier>(); }

            return suppliers;
        }

        [HttpPost(nameof(Create))]
        public async Task<Supplier> Create(CreateSupplierDTO createSupplierDTO)
        {
            if (!ModelState.IsValid) throw new NullReferenceException();

            return await supplierService.Create(createSupplierDTO);

        }
        [HttpPost(nameof(CreateMany))]

        public async Task<IEnumerable<Supplier>> CreateMany([FromBody]IEnumerable<CreateSupplierDTO> createSupplierDTOs)
        {
            if (!ModelState.IsValid) throw new NullReferenceException(ModelState.ErrorCount.ToString());

            return await supplierService.CreateMany(createSupplierDTOs);
        }

        [HttpPut(nameof(Update))]

        public Task<Supplier> Update(UpdateSupplierDTO updateSuplierDTO)
        {
            if (!ModelState.IsValid) throw new NullReferenceException(ModelState.ErrorCount.ToString());

            return supplierService.Update(updateSuplierDTO);
        }
        [HttpPut(nameof(UpdateMany))]
        public Task<IEnumerable<Supplier>> UpdateMany([FromBody]IEnumerable<UpdateSupplierDTO> updateSuplierDTOs)
        {
            if (!ModelState.IsValid) throw new NullReferenceException(ModelState.ErrorCount.ToString());

            return supplierService.UpdateMany(updateSuplierDTOs);
        }


        [HttpDelete("Delete/{id}")]
        public async Task<bool> Delete(int id)
        {
            bool result = await supplierService.Delete(id);

            return result ? true : false;
        }


        [HttpDelete(nameof(DeleteMany))]
        public async Task<bool> DeleteMany([FromBody]IEnumerable<int> ids)
        {
            bool result = await supplierService.DeleteMany(ids);

            return result ? true : false;
        }




    }
}
