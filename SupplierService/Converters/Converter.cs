using StokKontrol.Data;
using SupplierService.Models.DTOs;
using System.Diagnostics.Metrics;
using System.Net;
using System.Xml.Linq;

namespace SupplierService.Converters
{
    public class Converter
    {
        private static readonly Lazy<Converter> lazy_singleInstance = new Lazy<Converter>(() => new Converter(), true);
        private Converter()
        {
            
        }

        public static Converter GetInstance()
        {
            return lazy_singleInstance.Value;
        }

        public Supplier Convert(CreateSupplierDTO createSupplierDTO)
        {
            return new Supplier()
            {
                Name         = createSupplierDTO.Name        ,
                Address      = createSupplierDTO.Address     ,
                City         = createSupplierDTO.City        ,
                Country      = createSupplierDTO.Country     ,
                PhoneNumber  = createSupplierDTO.PhoneNumber ,
            };
        }
        public IEnumerable<Supplier> Convert(IEnumerable<CreateSupplierDTO> createSupplierDTOs)
        {
            List<Supplier> suppliers = new List<Supplier>();
            foreach (var item in createSupplierDTOs)
                suppliers.Add(new Supplier() {
                    Name         = item.Name            ,
                    Address      = item.Address         ,
                    City         = item.City            ,
                    Country      = item.Country         ,
                    PhoneNumber  = item.PhoneNumber     ,
                });

            return suppliers;
        }
        public Supplier Convert(UpdateSupplierDTO updateSupplierDTO, Supplier baseSupplier)
        {
            baseSupplier.Name         = updateSupplierDTO.Name        ;
            baseSupplier.Address      = updateSupplierDTO.Address     ;
            baseSupplier.City         = updateSupplierDTO.City        ;
            baseSupplier.Country      = updateSupplierDTO.Country     ;
            baseSupplier.PhoneNumber  = updateSupplierDTO.PhoneNumber ;

            return baseSupplier;
        }
        public IEnumerable<Supplier> Convert(IEnumerable<UpdateSupplierDTO> updateSupplierDTOs, IEnumerable<Supplier> baseSuppliers)
        {
            var listToDictionary = baseSuppliers.ToDictionary(x => x.Id, x => x);
            foreach (var item in updateSupplierDTOs)
                // Dictionary içerisindeki value'lerde DTOs'dan gelen Id varsa if'e gir. If içerisinde de ilgili key'e ait entity nesnesini kullan
                if(listToDictionary.TryGetValue(item.Id,out Supplier supplier)) 
                {
                    supplier.Name       = item.Name         ;
                    supplier.Address    = item.Address      ;
                    supplier.City       = item.City         ;
                    supplier.Country    = item.Country      ;
                    supplier.PhoneNumber= item.PhoneNumber  ;
                }


            return listToDictionary.Values;
        }

    }
}
