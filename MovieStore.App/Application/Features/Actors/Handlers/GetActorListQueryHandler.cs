using Application.Features.Actors.Business;
using Application.Features.Actors.Dtos;
using Application.Features.Actors.Models;
using Application.Features.Actors.Queries;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Actors.Handlers
{
    public class GetActorListQueryHandler : ActorUseCase, IRequestHandler<GetListActorQuery, GetListActorQueryViewModel>
    {
        public GetActorListQueryHandler(IActorRepository actorRepository) : base(actorRepository)
        {

        }
        public async Task<GetListActorQueryViewModel> Handle(GetListActorQuery request, CancellationToken cancellationToken)
        {
            GetListActorQueryViewModel response = new GetListActorQueryViewModel();

            var actors = await Repository.GetList(include: (x => x.Include(a => a.Movies)));
            foreach (var actor in actors)
            {

                var actorDto = new GetListActorQueryDto
                {
                    Id = EncryptionService.Encrypt(actor.Id),
                    FirstName = actor.FirstName,
                    LastName = actor.LastName,
                    Movies = actor.Movies,
                };
                response.Actors.Add(actorDto);

            }
            return response;

        }
    }
}
