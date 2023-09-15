using Application.Features.Tokens.Roles;
using Application.Features.Users.Business;
using Application.Features.Users.Commands;
using Application.Features.Users.Models;
using Application.Services.Repositories;
using Core.Application;
using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Handlers
{
    public class CreateUserCommandHandler : UserUseCase, IRequestHandler<CreateUserCommand, CreatedUserViewModel>
    {
        private readonly ICustomerRepository _customerRepository;
        public CreateUserCommandHandler(IUserRepository userRepository, ICustomerRepository customerRepository) : base(userRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CreatedUserViewModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await CheckIfUserExistsWithEmail(request.Email);
            var customer = new Customer
            {
                FirstName = request.Name,
                LastName = request.Surname,
                IsActive = true,
            };
            var createdCustomer = await _customerRepository.AddAsync(customer);
            var user = new User
            {
                Surname = request.Surname,
                Email = request.Email,
                Password = request.Password,
                Name = request.Name,
                CustomerId = createdCustomer.Id,
                Roles=RoleDefinition.Customer
            };

            var createdUser = await Repository.AddAsync(user);
            CreatedUserViewModel response = new()
            {
                Email = request.Email,
                FirstName = request.Name,
                LastName = request.Surname,
                Id = EncryptionService.Encrypt(createdUser.Id),
                Message = $"User with Email:{request.Email}" + Messages.CreatedMessage
            };
            return response;    
        }
    }
}
