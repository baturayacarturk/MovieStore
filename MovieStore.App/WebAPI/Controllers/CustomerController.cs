using Application.Features.Customers.Commands;
using Application.Features.Customers.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class CustomerController : BaseController
    {
        [HttpPost("Create")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateCustomer(CreateCustomerCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("Delete")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteCustomer(DeleteCustomerCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("Update")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("GetCustomer")]
        [Authorize(Roles = "admin, customer")]

        public async Task<IActionResult> GetCustomer(GetCustomerQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
