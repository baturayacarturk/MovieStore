using Application.Features.Tokens.Models;
using Application.Features.Tokens.Roles;
using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Tokens.Handlers
{
    public class TokenHandler
    {
        private readonly IConfiguration _configuration;
        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token CreateAccessToken(User user)
        {
            Token tokenModel = new Token();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            tokenModel.ExpirationDate = DateTime.Now.AddMinutes(50);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,EncryptionService.Encrypt(user.CustomerId)),
                new Claim(ClaimTypes.Role,user.Roles)
            };
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: _configuration["Token:Issuer"],
                claims:claims,
                audience: _configuration["Token:Audience"],
                expires: tokenModel.ExpirationDate,
                notBefore: DateTime.Now,
                signingCredentials: credentials)
                ;
            JwtSecurityTokenHandler tokenHandler = new();
            tokenModel.AccessToken = tokenHandler.WriteToken(securityToken);
            tokenModel.RefreshToken = CreateRefreshToken();
            return tokenModel;
        }
        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
