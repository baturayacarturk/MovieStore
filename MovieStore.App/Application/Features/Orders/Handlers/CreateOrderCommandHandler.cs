using Application.Features.Movies.Business;
using Application.Features.Orders.Business;
using Application.Features.Orders.Commands;
using Application.Features.Orders.Dtos;
using Application.Features.Orders.Models;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Handlers
{
    public class CreateOrderCommandHandler : OrderUseCase, IRequestHandler<CreateOrderCommand, CreatedOrderViewModel>
    {
        private readonly MovieUseCase _movieUseCase;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMovieRepository _movieRepository;
        public CreateOrderCommandHandler(IOrderRepository repository, MovieUseCase movieUseCase, IHttpContextAccessor contextAccessor, IMovieRepository movieRepository) : base(repository)
        {
            _movieUseCase = movieUseCase;
            _contextAccessor = contextAccessor;
            _movieRepository = movieRepository;
        }

        public async Task<CreatedOrderViewModel> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var movieId = EncryptionService.Decrypt(request.MovieId);
            await _movieUseCase.MovieMustActive(movieId);
            await _movieUseCase.MustExistsCheckWithId(movieId);
            var customerId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault().Value;

            var movieToOrder = await _movieRepository.Get(x => x.Id == movieId);
            movieToOrder.IsActive = false;
            movieToOrder.CustomerId = EncryptionService.Decrypt(customerId);
            var orderedMovie = await _movieRepository.UpdateAsync(movieToOrder);
            Order orderToProcess = new()
            {
                CustomerId = EncryptionService.Decrypt(customerId),
                MovieId = movieId,
                OrderDate = DateTime.Now,
                Price = movieToOrder.Price,
            };
            var createdOrder = await Repository.AddAsync(orderToProcess);
            CreateOrderMovieDto movieDto = new()
            {
                Name = movieToOrder.Name,
                Price = movieToOrder.Price,
            };
            CreatedOrderViewModel response = new()
            {
                Id = EncryptionService.Encrypt(createdOrder.Id),
                Movie = movieDto,
                OrderDate = DateTime.Now.ToString("dd/MM/yyyy")
            };
            return response;
        }
    }
}
