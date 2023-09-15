using Application.Features.Orders.Commands;
using Application.Features.Orders.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace WebAPI.Controllers
{

    public class OrderController : BaseController
    {
        [HttpPost("CreateOrder")]
        [Authorize(Roles = "admin, customer")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }
        [HttpPost("GetOrder")]
        [Authorize(Roles = "admin, customer")]

        public async Task<IActionResult> GetOrders(GetOrderQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        [HttpPost("GetOrderList")]
        [Authorize(Roles = "admin, customer")]

        public async Task<IActionResult> GetOrderList(GetOrderListQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
