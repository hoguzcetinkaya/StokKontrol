using CustomerService.Models.DTOs;
using StokKontrol.Data;

namespace CustomerService.Converters
{
    public class Converter
    {
        private static readonly Lazy<Converter> lazy_singleInstance = new Lazy<Converter>(() => new Converter(),true);
        private Converter()
        {
            
        }

        public static Converter GetInstance()
        {
            return lazy_singleInstance.Value;
        }

        public Customer Convert(CreateCustomerDTO createCustomerDTO)
        {
            return new Customer()
            {
                Name         = createCustomerDTO.Name       ,
                Address      = createCustomerDTO.Address    ,
                City         = createCustomerDTO.City       ,
                Country      = createCustomerDTO.Country    ,
                Email        = createCustomerDTO.Email      ,
                PhoneNumber  = createCustomerDTO.PhoneNumber,
            };
        }

        public IEnumerable<Customer> Convert(IEnumerable<CreateCustomerDTO> createCustomerDTOs)
        {
            List<Customer> customers= new List<Customer>();
            foreach (var item in createCustomerDTOs)
            {
                customers.Add(new Customer()
                {
                    Name         = item.Name       ,
                    Address      = item.Address    ,
                    City         = item.City       ,
                    Country      = item.Country    ,
                    Email        = item.Email      ,
                    PhoneNumber  = item.PhoneNumber,
                });
            }

            return customers;
        }

        public Customer Convert(UpdateCustomerDTO updateCustomerDTO, Customer baseCustomer)
        {
              baseCustomer.Name         = updateCustomerDTO.Name       ;
              baseCustomer.Address      = updateCustomerDTO.Address    ;
              baseCustomer.City         = updateCustomerDTO.City       ;
              baseCustomer.Country      = updateCustomerDTO.Country    ;
              baseCustomer.PhoneNumber  = updateCustomerDTO.PhoneNumber;
              baseCustomer.Email        = updateCustomerDTO.Email      ;

            return baseCustomer;
        }

        public IEnumerable<Customer> Convert(IEnumerable<UpdateCustomerDTO> updateCustomerDTOs, IEnumerable<Customer> baseCustomers)
        {
            // `baseCustomers`'ı daha hızlı arama yapabilmek için bir sözlüğe dönüştür
            var customerDict = baseCustomers.ToDictionary(c => c.Id, c => c);

            foreach (var dto in updateCustomerDTOs)
            {
                if (customerDict.TryGetValue(dto.Id, out var customer))
                {
                    // Eşleşen müşteriyi güncelle
                    customer.Name        = dto.Name       ;
                    customer.Address     = dto.Address    ;
                    customer.City        = dto.City       ;
                    customer.Country     = dto.Country    ;
                    customer.Email       = dto.Email      ;
                    customer.PhoneNumber = dto.PhoneNumber;
                }
            }

            return customerDict.Values;
        }
    }
}
