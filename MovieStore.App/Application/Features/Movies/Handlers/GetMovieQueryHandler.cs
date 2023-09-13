using Application.Features.Movies.Business;
using Application.Features.Movies.Models;
using Application.Features.Movies.Queries;
using Application.Features.SharedDtos;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Movies.Handlers
{
    public class GetMovieQueryHandler : MovieUseCase, IRequestHandler<GetMovieQuery, GetMovieQueryViewModel>
    {
        public GetMovieQueryHandler(IMovieRepository movieRepository) : base(movieRepository)
        {
        }

        public async Task<GetMovieQueryViewModel> Handle(GetMovieQuery request, CancellationToken cancellationToken)
        {
            var id = EncryptionService.Decrypt(request.Id);
            await MustExistsCheckWithId(id);
            var movie = await Repository.GetWithInclude(x => x.Id == id, x => x.Actors);
            GetMovieQueryViewModel response = new()
            {
                CustomerId = movie.CustomerId.HasValue ? EncryptionService.Encrypt(movie.CustomerId.Value) : null,
                DirectorId = EncryptionService.Encrypt(movie.DirectorId),
                Id = EncryptionService.Encrypt(id),
                IsActive = movie.IsActive,
                Name = movie.Name,
                Price = movie.Price,
                Type = movie.Type,
            };
            foreach (var item in movie.Actors)
            {
                response.Actors.Add(new GetActorQueryDto
                {
                    Id = EncryptionService.Encrypt(item.Id),
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                });
            }
            return response;
        }
    }
}
