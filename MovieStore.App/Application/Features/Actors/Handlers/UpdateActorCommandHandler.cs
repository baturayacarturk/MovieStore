using Application.Features.Actors.Business;
using Application.Features.Actors.Commands;
using Application.Features.Actors.Models;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Actors.Handlers
{
    public class UpdateActorCommandHandler : ActorUseCase, IRequestHandler<UpdateActorCommand, UpdatedActorViewModel>
    {
        public UpdateActorCommandHandler(IActorRepository actorRepository) : base(actorRepository)
        {
        }

        public async Task<UpdatedActorViewModel> Handle(UpdateActorCommand request, CancellationToken cancellationToken)
        {
            int id = EncryptionService.Decrypt(request.Id);
            await MustExistsCheckWithId(id);
            var actor = await Repository.GetWithInclude(x=>x.Id==id, x => x.Movies);
            var originalFirstName= actor.FirstName;
            var originalLastName= actor.LastName;   
            actor.FirstName = request.FirstName != default ? request.FirstName : actor.FirstName;
            actor.LastName = request.LastName != default ? request.LastName : actor.LastName;
            var updatedActor = await Repository.UpdateAsync(actor);
            var diff = updatedActor.Movies.Where(updatedMovie => !actor.Movies.Any(existingMovie => existingMovie.Id == updatedMovie.Id)).ToList();
            UpdatedActorViewModel response = new()
            {
                FirstName = originalFirstName,
                LastName = originalLastName,
                Movies = (List<Domain.Entities.Movie>)actor.Movies,
                UpdatedFirstName = updatedActor.FirstName != originalFirstName ? updatedActor.FirstName : "First Name did not changed",
                UpdatedLastName = updatedActor.LastName != originalLastName ? updatedActor.LastName : "Last Name did not changed",
                UpdatedMovies = diff.Any() ? diff.ToList() : new List<Movie>(),
                IsMoviesUpdated = diff.Any() ? true : false

            };
            return response;
        }
    }
}
