using Application.Features.Actors.Business;
using Application.Features.Actors.Commands;
using Application.Features.Actors.Models;
using Application.Features.Directors.Business;
using Application.Features.Directors.Commands;
using Application.Features.Directors.Models;
using Application.Services.Repositories;
using Core.Application;
using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Directors.Handlers
{
    public class DeleteDirectorCommandHandler : DirectorUseCase, IRequestHandler<DeleteDirectorCommand, DeletedDirectorViewModel>
    {
        public DeleteDirectorCommandHandler(IDirectorRepository directorRepository) : base(directorRepository)
        {
        }

        public async Task<DeletedDirectorViewModel> Handle(DeleteDirectorCommand request, CancellationToken cancellationToken)
        {
            var id = EncryptionService.Decrypt(request.Id);
            await DirectorMustExist(id);
            var director = await Repository.Get(x=>x.Id==id);
            var result = await Repository.DeleteAsync(director);
            DeletedDirectorViewModel response = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Message = request.FirstName + " " + request.LastName + Messages.DeletedMessage,
            };
            return response;
        }
    }
}
