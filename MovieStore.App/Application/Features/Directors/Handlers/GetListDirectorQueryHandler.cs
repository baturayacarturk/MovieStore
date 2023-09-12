using Application.Features.Actors.Dtos;
using Application.Features.Actors.Models;
using Application.Features.Directors.Business;
using Application.Features.Directors.Dtos;
using Application.Features.Directors.Models;
using Application.Features.Directors.Queries;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Directors.Handlers
{
    public class GetListDirectorQueryHandler : DirectorUseCase, IRequestHandler<GetListDirectorQuery, GetListDirectorQueryViewModel>
    {
        public GetListDirectorQueryHandler(IDirectorRepository directorRepository) : base(directorRepository)
        {
        }

        public async Task<GetListDirectorQueryViewModel> Handle(GetListDirectorQuery request, CancellationToken cancellationToken)
        {

            GetListDirectorQueryViewModel response = new GetListDirectorQueryViewModel();

            var directors = await Repository.GetList(include: x => x.Include(a => a.ProducedMovies));
            foreach (var director in directors)
            {

                var directorDto = new GetListDirectorQueryDto
                {
                    Id = EncryptionService.Encrypt(director.Id),
                    FirstName = director.FirstName,
                    LastName = director.LastName,
                    Movies = director.ProducedMovies,
                };
                response.Directors.Add(directorDto);

            }
            return response;
        }
    }
}
