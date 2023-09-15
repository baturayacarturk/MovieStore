using Application.Features.Tokens.Handlers;
using Application.Features.Tokens.Models;
using Application.Features.Users.Business;
using Application.Features.Users.Commands;
using Application.Features.Users.Models;
using Application.Services.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Handlers
{
    public class LoginCommandHandler : UserUseCase, IRequestHandler<LoginCommand, LoggedInViewModel>
    {
        private readonly IConfiguration _configuration;
        public LoginCommandHandler(IUserRepository userRepository, IConfiguration configuration) : base(userRepository)
        {
            _configuration = configuration;
        }

        public async Task<LoggedInViewModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            await PasswordAndEmailMustCorrect(request.Email,request.Password);
            TokenHandler handler = new TokenHandler(_configuration);
            var user = await Repository.Get(x => x.Email == request.Email);
            Token token = handler.CreateAccessToken(user);
            user.RefreshToken=token.RefreshToken;
            user.RefreshTokenExpireDate = token.ExpirationDate.AddMinutes(5);
            await Repository.UpdateAsync(user);
            LoggedInViewModel response = new()
            {
                Token = token
            };
            return response;
        }
    }
}
