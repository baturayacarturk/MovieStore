using Application.Features.Actors.Business;
using Application.Features.Directors.Business;
using Application.Features.Movies.Business;
using Application.Features.Movies.Commands;
using Application.Features.Movies.Models;
using Application.Features.SharedDtos;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Movies.Handlers
{
    public class CreateMovieCommandHandler : MovieUseCase, IRequestHandler<CreateMovieCommand, CreatedMovieViewModel>
    {
        private readonly DirectorUseCase _directorUseCase;
        private readonly ActorUseCase _actorUseCase;
        private readonly IActorRepository _actorRepository;


        public CreateMovieCommandHandler(IMovieRepository movieRepository, DirectorUseCase directorUseCase, ActorUseCase actorUseCase, IActorRepository actorRepository) : base(movieRepository)
        {
            _directorUseCase = directorUseCase;
            _actorUseCase = actorUseCase;
            _actorRepository = actorRepository;
        }

        public async Task<CreatedMovieViewModel> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var directorId = EncryptionService.Decrypt(request.DirectorId);
            await MovieShouldNotExists(request.Name, request.DirectorId);
            await _directorUseCase.MustExistsCheckWithId(directorId);
            await _actorUseCase.ActorsMustExist(request.Actors);
            List<Actor> actors = new List<Actor>();
            foreach (ActorsDto actor in request.Actors)
            {
                var id = EncryptionService.Decrypt(actor.Id);
                var toBeAddedActor = await _actorRepository.GetWithInclude(x => x.Id == id, x => x.Movies);
                actors.Add(toBeAddedActor);
            }
            var movie = await Repository.AddAsync(new Movie
            {
                Name = request.Name,
                Type = request.Type,
                DirectorId = directorId,
                Actors = actors,
                Price = request.Price,
                IsActive = true
            });
            CreatedMovieViewModel response = new()
            {
                Name = request.Name,
                DirectorId = EncryptionService.Encrypt(movie.DirectorId),
                Price = movie.Price,
                Type = request.Type,
            };
            return response;
        }
    }
}
