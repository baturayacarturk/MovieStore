using Application.Features.Tokens.Handlers;
using Application.Features.Tokens.Models;
using Application.Features.Users.Business;
using Application.Features.Users.Models;
using Application.Features.Users.Queries;
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
    public class GetRefreshTokenQueryHandler :UserUseCase, IRequestHandler<GetRefreshTokenQuery, GetRefreshTokenViewModel>
    {
        private readonly IConfiguration _configuration;
        public GetRefreshTokenQueryHandler(IUserRepository userRepository, IConfiguration configuration) : base(userRepository)
        {
            _configuration = configuration;
        }

        public async Task<GetRefreshTokenViewModel> Handle(GetRefreshTokenQuery request, CancellationToken cancellationToken)
        {
            await CheckRefreshToken(request.Token);
            TokenHandler handler = new TokenHandler(_configuration);
            var user = await Repository.Get(x => x.RefreshToken==request.Token);
            Token token = handler.CreateAccessToken(user);
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = token.ExpirationDate.AddMinutes(5);
            await Repository.UpdateAsync(user);
            GetRefreshTokenViewModel response = new()
            {
                RefreshToken = token
            };
            return response;
        }
    }
}
