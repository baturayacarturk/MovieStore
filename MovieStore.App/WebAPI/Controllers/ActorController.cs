using Application.Features.Actors.Commands;
using Application.Features.Actors.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    public class ActorController : BaseController
    {

        [HttpPost("Create")]
        public async Task<IActionResult> CreateActor(CreateActorCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateActor(UpdateActorCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteActor(DeleteActorCommand command)
        {
            var result = await Mediator.Send(command);  
            return Ok(result);
        }
        [HttpPost("GetActor")]
        public async Task<IActionResult> GetActor(GetActorQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);  
        }
        [HttpPost("GetList")]
        public async Task<IActionResult>GetList(GetListActorQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
