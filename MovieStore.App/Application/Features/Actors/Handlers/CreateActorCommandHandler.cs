using Application.Features.Actors.Business;
using Application.Features.Actors.Commands;
using Application.Features.Actors.Models;
using Application.Services.Repositories;
using Core.Application;
using Domain.Entities;
using MediatR;

namespace Application.Features.Actors.Handlers
{
    public class CreateActorCommandHandler : ActorUseCase,IRequestHandler<CreateActorCommand, CreatedActorViewModel>
    {
        
        public CreateActorCommandHandler(IActorRepository actorRepository):base(actorRepository)
        {

      
        }

        public async Task<CreatedActorViewModel> Handle(CreateActorCommand request, CancellationToken cancellationToken)
        {
            Actor actor =new Actor
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
            };
            var result = await Repository.AddAsync(actor);
            CreatedActorViewModel response = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Message = request.FirstName + " " + request.LastName + Messages.CreatedMessage,
            };
            return response;
        }
    }
}
