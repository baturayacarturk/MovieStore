using Application.Features.Users.Commands;
using Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Context;

namespace WebAPI.Controllers
{
    public class UserController : BaseController
    {

        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }
        [HttpPost("GetRefreshToken")]
        public async Task<IActionResult> GetRefreshToken(GetRefreshTokenQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
