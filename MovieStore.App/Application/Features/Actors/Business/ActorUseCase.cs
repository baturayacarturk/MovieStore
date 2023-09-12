using Application.BaseUseCase;
using Application.Features.Actors.Commands;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
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
        public  async Task ActorMustExist(int id)
        {
            var actorExists = await Repository.Get(x=>x.Id==id);
            if (actorExists is null) throw new BusinessException("Actor is not exists.");    
        }
    }
}
