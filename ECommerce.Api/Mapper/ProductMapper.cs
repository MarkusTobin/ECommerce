using ECommerce.Api.Dtos;
using ECommerce.Api.Entities;
using Mapster;

namespace ECommerce.Api.Mapper
{
    public static class ProductMapper
    {
        public static ProductDto ToProductDto(this Product product)
        {
            return product.Adapt<ProductDto>();
        }

        public static Product ToProduct(this ProductDto productDto)
        {
            return productDto.Adapt<Product>();
        }

        public static IEnumerable<ProductDto> ToProductDtos(this IEnumerable<Product> products)
        {
            return products.Adapt<IEnumerable<ProductDto>>();
        }

        public static void UpdateProduct(this Product product, ProductDto productDto)
        {
            productDto.Adapt(product);
        }
    }
}
