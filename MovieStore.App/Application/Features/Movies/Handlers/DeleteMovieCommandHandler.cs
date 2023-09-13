using Application.Features.Movies.Business;
using Application.Features.Movies.Commands;
using Application.Features.Movies.Models;
using Application.Services.Repositories;
using Core.Application;
using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Movies.Handlers
{
    public class DeleteMovieCommandHandler : MovieUseCase, IRequestHandler<DeleteMovieCommand, DeletedMovieViewModel>
    {
        public DeleteMovieCommandHandler(IMovieRepository movieRepository) : base(movieRepository)
        {
        }

        public async Task<DeletedMovieViewModel> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var id = EncryptionService.Decrypt(request.Id);
            await MustExistsCheckWithId(id);
            var movieToDelete = await Repository.Get(x=>x.Id==id);
            movieToDelete.IsActive = false;
            var deletedMovie = await Repository.UpdateAsync(movieToDelete);
            DeletedMovieViewModel response = new()
            {
                Name = deletedMovie.Name,
                Message = deletedMovie.Name + Messages.DeletedMessage
            };
            return response;
        }
    }
}
