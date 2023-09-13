using Application.Features.Actors.Models;
using Application.Features.Movies.Business;
using Application.Features.Movies.Dtos;
using Application.Features.Movies.Models;
using Application.Features.Movies.Queries;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Movies.Handlers
{
    public class GetListMovieQueryHandler : MovieUseCase, IRequestHandler<GetListMovieQuery, GetListMovieQueryViewModel>
    {
        public GetListMovieQueryHandler(IMovieRepository movieRepository) : base(movieRepository)
        {
        }
        public async Task<GetListMovieQueryViewModel>Handle(GetListMovieQuery request, CancellationToken cancellationToken)
        {
            GetListMovieQueryViewModel response = new GetListMovieQueryViewModel();
            var movies = await Repository.GetList(include: x => x.Include(x => x.Actors));
            var movieQueryDtos = new List<GetListMovieQueryDto>();

            foreach (var movie in movies)
            {
                var movieQueryDto = new GetListMovieQueryDto
                {
                    DirectorId = EncryptionService.Encrypt(movie.DirectorId),
                    Id = EncryptionService.Encrypt(movie.Id),
                    IsActive = movie.IsActive,
                    Name = movie.Name,
                    Price = movie.Price,
                    Type = movie.Type,
                };
                foreach (var actor in movie.Actors)
                {
                    var actorDto = new GetListMovieQueryActorDto
                    {
                        Id = EncryptionService.Encrypt(actor.Id),
                        FirstName = actor.FirstName,
                        LastName = actor.LastName,
                    };
                    movieQueryDto.Actors.Add(actorDto);
                }

                movieQueryDtos.Add(movieQueryDto);
            }
            response.Movies = movieQueryDtos;

            return response;
        }
    }
}
