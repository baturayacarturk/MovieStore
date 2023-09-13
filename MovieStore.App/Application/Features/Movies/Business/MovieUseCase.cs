using Application.BaseUseCase;
using Application.Features.Movies.Dtos;
using Application.Features.SharedDtos;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using Core.Persistence.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Movies.Business
{
    public class MovieUseCase : BaseUseCase<Movie, IMovieRepository>
    {
        public MovieUseCase(IMovieRepository movieRepository) : base(movieRepository) { }


        public override async Task MustExistsCheckWithId(int id)
        {
            var directorExists = await Repository.Get(x => x.Id == id);
            if (directorExists is null) throw new BusinessException("Movie is not exists.");

        }
        public async Task MovieShouldNotExists(string name, string directorId)
        {
            var id = EncryptionService.Decrypt(directorId);
            var movieExists = await Repository.Get(x => x.Name == name && x.DirectorId == id);
            if (movieExists is not null) throw new BusinessException($"Movie {name} is already added.");
        }
        public async Task MoviesMustExists(ICollection<UpdatedMovieCommandActorList> movies)
        {
            foreach (var movie in movies)
            {
                await MustExistsCheckWithId(EncryptionService.Decrypt(movie.Id));
            }
        }
    }
}
