using Application.Features.Actors.Business;
using Application.Features.Actors.Commands;
using Application.Features.Actors.Models;
using Application.Services.Repositories;
using Core.Application;
using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Actors.Handlers
{
    public class DeleteActorCommandHandler : ActorUseCase,IRequestHandler<DeleteActorCommand, DeletedActorViewModel>
    {
        public DeleteActorCommandHandler(IActorRepository actorRepository):base(actorRepository)
        {
            
        }

        public async Task<DeletedActorViewModel> Handle(DeleteActorCommand request, CancellationToken cancellationToken)
        {
            int id = EncryptionService.Decrypt(request.Id);
            await MustExistsCheckWithId(id);
            var actor = await Repository.Get(x=>x.Id== id);
            var result = await Repository.DeleteAsync(actor);
            DeletedActorViewModel response = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Message = request.FirstName + " " + request.LastName + Messages.DeletedMessage,
            };
            return response;
        }
    }
}
