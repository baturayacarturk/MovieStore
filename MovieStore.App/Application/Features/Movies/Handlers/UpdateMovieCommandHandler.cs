using Application.Features.Actors.Business;
using Application.Features.Movies.Business;
using Application.Features.Movies.Commands;
using Application.Features.Movies.Dtos;
using Application.Features.Movies.Models;
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
    public class UpdateMovieCommandHandler : MovieUseCase, IRequestHandler<UpdateMovieCommand, UpdatedMovieViewModel>
    {
        private readonly IActorRepository _actorRepository;
        private readonly ActorUseCase _actorUseCase;
        public UpdateMovieCommandHandler(IMovieRepository movieRepository, IActorRepository actorRepository, ActorUseCase actorUseCase) : base(movieRepository)
        {
            _actorRepository = actorRepository;
            _actorUseCase = actorUseCase;
        }

        public async Task<UpdatedMovieViewModel> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var id = EncryptionService.Decrypt(request.Id);
            await MustExistsCheckWithId(id);
            if(request.Actors.Any())
            {
                await _actorUseCase.ActorsMustExist(request.Actors);
            }
            var movie = await Repository.GetWithInclude(x => x.Id == id, x => x.Actors);
            var originalName = movie.Name;
            var originalPrice = movie.Price;
            movie.DirectorId= request.DirectorId!=default?EncryptionService.Decrypt(request.DirectorId):movie.DirectorId;
            movie.Name = request.Name!=default? request.Name:movie.Name;
            movie.Type = (Domain.Enums.MovieType)(request.Type!=default?request.Type:movie.Type);
            var actors = new List<Actor>();
            if(request.Actors.Any())
            {
                foreach (var actor in request.Actors)
                {
                    var found = await _actorRepository.Get(x => x.Id == EncryptionService.Decrypt(actor.Id));
                    actors.Add(found);
                }
            }
           
            movie.Actors = !request.Actors.Any()? movie.Actors : actors;
            movie.Price = request.Price is null ? movie.Price: request.Price.Value;
            var originalActorsDto = new List<UpdatedMovieCommandActorList>();
            foreach (var actor in movie.Actors)
            {
                originalActorsDto.Add(new UpdatedMovieCommandActorList
                {
                    Id = EncryptionService.Encrypt(actor.Id),
                    FirstName = actor.FirstName,
                    LastName = actor.LastName,
                });
            }

            var updatedMovie = await Repository.UpdateAsync(movie);
            var updatedActorsDto = new List<UpdatedMovieCommandActorList>();
            if(request.Actors.Any())
            {
                foreach (var actor in actors)
                {
                    updatedActorsDto.Add(new UpdatedMovieCommandActorList
                    {
                        Id = EncryptionService.Encrypt(actor.Id),
                        FirstName = actor.FirstName,
                        LastName = actor.LastName,
                    });
                }
            }

            UpdatedMovieViewModel response = new()
            {
                DirectorId = EncryptionService.Encrypt(movie.DirectorId),
                Name = originalName,
                Type = movie.Type,
                Price = originalPrice,
                UpdatedName = request.Name is null ? null : request.Name,
                UpdatedMovieType = request.Type is null ? null : request.Type,
                Actors = originalActorsDto,
                UpdatedActors = request.Actors.Any() ? updatedActorsDto : new List<UpdatedMovieCommandActorList>(),
                UpdatedDirectorId = request.DirectorId is not null ? EncryptionService.Encrypt(movie.DirectorId) : null,
                UpdatedPrice = request.Price is null ? null : request.Price,
            };
            return response;
        }
    }
}
