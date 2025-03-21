using ECommerce.Api.Dtos;
using ECommerce.Api.DTOs;
using ECommerce.Api.Entities;
using ECommerce.Api.Interface.IService;
using ECommerce.Api.Mapper;
using ECommerce.Api.Repository;
using ECommerce.Api.Services;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController(OrderService orderService) : ControllerBase
    {
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetOrdersByCustomerId(string customerId)
        {
            var orders = await orderService.GetOrdersByCustomerIdAsync(customerId);
            return Ok(orders);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var order = await orderService.GetOrderAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderDto orderDto)
        {
            var order = await orderService.CreateOrderAsync(orderDto);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

    }
}