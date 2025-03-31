using ECommerce.Shared.Dtos;
using ECommerce.Api.Entities;
using Mapster;

namespace ECommerce.Api.Mapper
{
    public static class OrderMapper
    {
        public static OrderDto ToOrderDto(this Order order)
        {
            return order.Adapt<OrderDto>();
        }

        public static Order ToOrder(this OrderDto orderDto)
        {
            return orderDto.Adapt<Order>();
        }
        public static IEnumerable<OrderDto> ToOrderDtos(this IEnumerable<Order> orders)
        {
            return orders.Adapt<IEnumerable<OrderDto>>();
        }
    }
}
