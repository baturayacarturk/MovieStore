using Application.Features.Actors.Models;
using Application.Features.Directors.Business;
using Application.Features.Directors.Commands;
using Application.Features.Directors.Models;
using Application.Services.Repositories;
using Core.Application;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Directors.Handlers
{
    public class CreateDirectorCommandHandler:DirectorUseCase,IRequestHandler<CreateDirectorCommand,CreatedDirectorViewModel>
    {
        public CreateDirectorCommandHandler(IDirectorRepository directorRepository):base(directorRepository) 
        {
            
        }

        public async Task<CreatedDirectorViewModel> Handle(CreateDirectorCommand request, CancellationToken cancellationToken)
        {
            Director director = new Director
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
            };
            var result = await Repository.AddAsync(director);
            CreatedDirectorViewModel response = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Message = request.FirstName + " " + request.LastName + Messages.CreatedMessage,
            };
            return response;
        }
    }
}
