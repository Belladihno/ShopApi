using ShopifyApi.DTOs.Products;
using ShopifyApi.Models;

namespace ShopifyApi.Mappings
{
    public static class ProductMappings
    {
        // convert DB model to response DTO
        public static ProductResponseDto ToResponseDto(this Product product)
        {
            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CreatedAt = product.CreatedAt,
            };
        }

        //convert create dto to db model
        public static Product ToModel(this CreateProductDto dto)
        {
            return new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
            };
        }

        //apply update dto changes to only existing db model
        public static void ApplyUpdate(this Product product, UpdateProductDto dto)
        {
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Stock = dto.Stock;
        }
    }
}
