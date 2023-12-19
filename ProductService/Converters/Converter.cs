using ProductService.Models.DTOs;
using ProductService.Models.ViewModels;
using StokKontrol.Data;

namespace ProductService.Converters
{
    public class Converter
    {
        private static readonly Lazy<Converter> lazy_singleInstance = new Lazy<Converter>(() => new Converter(), true);

        private                 /*Constructor*/ Converter()
        {
        }
        public static Converter GetInstance()
        {
            return lazy_singleInstance.Value;
        }
        public Product Convert(CreateProductDTO createProductDTO)
        {
            Product product         = new Product
            {
                Name            = createProductDTO.Name          ,
                Description     = createProductDTO.Description   ,
                Price           = createProductDTO.Price         ,
                StockQuantity   = createProductDTO.StockQuantity ,
                SupplierId      = createProductDTO.SupplierId    ,
                CategoryId      = createProductDTO.CategoryId    ,
            };

            return product;
        }
        public IEnumerable<Product> Convert(IEnumerable<CreateProductDTO> createProductDTOs)
        {
            List<Product> products = new List<Product>();
            foreach (var item in createProductDTOs)
            {
                products.Add(new Product
                {
                    Name            = item.Name          ,
                    Description     = item.Description   ,
                    Price           = item.Price         ,
                    StockQuantity   = item.StockQuantity ,
                    SupplierId      = item.SupplierId    ,
                    CategoryId      = item.CategoryId    ,
                });
            }

            return products;
        }

        public Product Convert(UpdateProductDTO updateProductDTO, Product baseProduct)
        {
            baseProduct.Name            = updateProductDTO.Name          ;
            baseProduct.Description     = updateProductDTO.Description   ;
            baseProduct.Price           = updateProductDTO.Price         ;
            baseProduct.StockQuantity   = updateProductDTO.StockQuantity ;

            return baseProduct;
        }

        public IEnumerable<Product> Convert(IEnumerable<UpdateProductDTO> updateProductDTOs, IEnumerable<Product> baseProducts)
        {
            Dictionary<int, Product> baseProductsToDict = baseProducts.ToDictionary(x => x.Id, x => x);

            foreach (var item in updateProductDTOs)
            {
                if(baseProductsToDict.TryGetValue(item.Id,out Product product))
                {
                    product.Name           = item.Name         ;
                    product.Description    = item.Description  ;
                    product.Price          = item.Price        ;
                    product.StockQuantity  = item.StockQuantity;
                }
            }

            return baseProductsToDict.Values;
        }

    }
}
