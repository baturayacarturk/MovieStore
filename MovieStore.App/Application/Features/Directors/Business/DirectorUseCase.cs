using Application.BaseUseCase;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Directors.Business
{
    public class DirectorUseCase : BaseUseCase<Director, IDirectorRepository>
    {
        public DirectorUseCase(IDirectorRepository directorRepository) : base(directorRepository) 
        { 
        }
        public async Task DirectorMustExist(int id)
        {
            var directorExists = await Repository.Get(x => x.Id==id);
            if (directorExists is null) throw new BusinessException("Director is not exists.");

        }
      

    }
}
