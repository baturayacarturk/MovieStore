using Application.Features.Actors.Business;
using Application.Features.Actors.Commands;
using Application.Features.Actors.Models;
using Application.Features.Directors.Business;
using Application.Features.Directors.Commands;
using Application.Features.Directors.Models;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using Core.Persistence.Repositories;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Directors.Handlers
{
    public class UpdateDirectorCommandHandler : DirectorUseCase, IRequestHandler<UpdateDirectorCommand, UpdatedDirectorViewModel>
    {
        public UpdateDirectorCommandHandler(IDirectorRepository directorRepository) : base(directorRepository)
        {
        }

        public async Task<UpdatedDirectorViewModel> Handle(UpdateDirectorCommand request, CancellationToken cancellationToken)
        {
            int id = EncryptionService.Decrypt(request.Id);
            await MustExistsCheckWithId(id);
            var director = await Repository.GetWithInclude(x => x.Id == id, x => x.ProducedMovies);
            var originalFirstName = director.FirstName;
            var originalLastName = director.LastName;
            director.FirstName = request.FirstName != default ? request.FirstName : director.FirstName;
            director.LastName = request.LastName != default ? request.LastName : director.LastName;
            var updatedActor = await Repository.UpdateAsync(director);
            var diff = updatedActor.ProducedMovies.Where(updatedMovie => !director.ProducedMovies.Any(existingMovie => existingMovie.Id == updatedMovie.Id)).ToList();
            UpdatedDirectorViewModel response = new()
            {
                FirstName = originalFirstName,
                LastName = originalLastName,
                Movies = (List<Domain.Entities.Movie>)director.ProducedMovies,
                UpdatedFirstName = updatedActor.FirstName != originalFirstName ? updatedActor.FirstName : "First Name did not changed",
                UpdatedLastName = updatedActor.LastName != originalLastName ? updatedActor.LastName : "Last Name did not changed",
                UpdatedMovies = diff.Any() ? diff.ToList() : new List<Movie>(),
                IsMoviesUpdated = diff.Any() ? true : false

            };
            return response;
        }
    }
}
