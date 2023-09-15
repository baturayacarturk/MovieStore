using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class ActorRepository : BaseRepository<Actor, MovieStoreDB>, IActorRepository
    {
        public ActorRepository(MovieStoreDB context) : base(context)
        {

        }
    }
}
