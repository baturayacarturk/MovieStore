using Application.Features.Actors.Business;
using Application.Features.Actors.Models;
using Application.Features.Actors.Queries;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Actors.Handlers
{
    public class GetActorQueryHandler:ActorUseCase,IRequestHandler<GetActorQuery,GetActorQueryViewModel>
    {
        public GetActorQueryHandler(IActorRepository actorRepository) :base(actorRepository)
        {
            
        }

        public async Task<GetActorQueryViewModel> Handle(GetActorQuery request, CancellationToken cancellationToken)
        {
            int id = EncryptionService.Decrypt(request.Id);
            await ActorMustExist(id);

            var actor = await Repository.GetWithInclude(x=>x.Id== id, x=>x.Movies);
            GetActorQueryViewModel response = new()
            {
                Id=EncryptionService.Encrypt(actor.Id),
                FirstName = actor.FirstName,
                LastName = actor.LastName,
                Movies = actor.Movies
            };
            return response;
        }
    }
}
