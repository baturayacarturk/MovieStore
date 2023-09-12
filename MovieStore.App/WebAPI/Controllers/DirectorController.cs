using Application.Features.Actors.Queries;
using Application.Features.Directors.Commands;
using Application.Features.Directors.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    public class DirectorController : BaseController
    {
        [HttpPost("CreateDirector")]
        public async Task<IActionResult> CreateDirector(CreateDirectorCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }
        [HttpPost("UpdateDirector")]
        public async Task<IActionResult> UpdateDirector(UpdateDirectorCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("DeleteDirector")]
        public async Task<IActionResult> DeleteDirector(DeleteDirectorCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("GetDirector")]
        public async Task<IActionResult> GetDirector(GetDirectorQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        [HttpPost("GetListDirector")]
        public async Task<IActionResult> GetListDirector(GetListDirectorQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
