using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Context;
using Persistence.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ActorRepository : BaseRepository<Actor, MovieStoreDB>, IActorRepository
    {
        public ActorRepository(MovieStoreDB context) : base(context)
        {

        }
    }
}
