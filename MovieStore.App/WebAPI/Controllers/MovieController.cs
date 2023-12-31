﻿using Application.Features.Movies.Commands;
using Application.Features.Movies.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class MovieController:BaseController
    {
        [HttpPost("CreateMovie")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateMovie(CreateMovieCommand command)
        {
            var result = await Mediator.Send(command);
            return Created("", result);
        }
        [HttpPost("DeleteMovie")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteMovie(DeleteMovieCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("GetMovie")]
        public async Task<IActionResult> GetMovie(GetMovieQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);  
        }
        [HttpPost("GetListMovie")]
        public async Task<IActionResult> GetListMovie(GetListMovieQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);  
        }
        [HttpPost("UpdateMovie")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateMovie(UpdateMovieCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
