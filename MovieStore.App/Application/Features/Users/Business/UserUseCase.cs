using Application.BaseUseCase;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Business
{
    public class UserUseCase : BaseUseCase<User, IUserRepository>
    {
        public UserUseCase(IUserRepository userRepository) : base(userRepository) { }

        public async Task CheckIfUserExistsWithEmail(string email)
        {
            var existUser = await Repository.Get(x=>x.Email == email);
            if (existUser is not null) throw new BusinessException("User is already exist");
        }
        public async Task PasswordAndEmailMustCorrect(string email,string password)
        {
            var user = await Repository.Get(x => x.Email == email&&x.Password==password);
            if (user is null) throw new BusinessException("Email or password is wrong");
        }
        public async Task CheckRefreshToken(string token)
        {
            var user=await Repository.Get(x => x.RefreshToken == token && x.RefreshTokenExpireDate > DateTime.Now);
            if (user is null) throw new BusinessException("There are no valid Refresh Token");
        }


    }
}
