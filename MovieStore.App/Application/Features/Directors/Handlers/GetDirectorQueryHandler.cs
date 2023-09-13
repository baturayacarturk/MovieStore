using Application.Features.Directors.Business;
using Application.Features.Directors.Models;
using Application.Features.Directors.Queries;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Directors.Handlers
{
    public class GetDirectorQueryHandler : DirectorUseCase, IRequestHandler<GetDirectorQuery, GetDirectorQueryViewModel>
    {
        public GetDirectorQueryHandler(IDirectorRepository directorRepository) : base(directorRepository)
        {
        }

        public async Task<GetDirectorQueryViewModel> Handle(GetDirectorQuery request, CancellationToken cancellationToken)
        {
            var id = EncryptionService.Decrypt(request.Id);
            await MustExistsCheckWithId(id);
            Director director = await Repository.GetWithInclude(x => x.Id == id, x => x.ProducedMovies);
            GetDirectorQueryViewModel response = new()
            {
                Id = EncryptionService.Encrypt(id),
                FirstName = director.FirstName,
                LastName = director.LastName,
                Movies = director.ProducedMovies
            };
            return response;
        }
    }
}
