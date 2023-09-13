using Application.BaseUseCase;
using Application.Features.Actors.Commands;
using Application.Features.SharedDtos;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Actors.Business
{
    public class ActorUseCase:BaseUseCase<Actor, IActorRepository>
    {
        public ActorUseCase(IActorRepository actorRepository):base(actorRepository) { }
        public override async Task MustExistsCheckWithId(int id)
        {
            var actorExists = await Repository.Get(x=>x.Id==id);
            if (actorExists is null) throw new BusinessException("Actor is not exists.");    
        }
        public async Task ActorsMustExist(ICollection<ActorsDto>? actors)
        {
            foreach(var actor in actors)
            {
                await MustExistsCheckWithId(EncryptionService.Decrypt(actor.Id));
            }
        }
    }
}
