using Application.Features.Orders.Business;
using Application.Features.Orders.Dtos;
using Application.Features.Orders.Models;
using Application.Features.Orders.Queries;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Handlers
{
    public class GetOrderQueryHandler : OrderUseCase, IRequestHandler<GetOrderQuery, GetOrderQueryViewModel>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMovieRepository _movieRepository;
        public GetOrderQueryHandler(IOrderRepository repository, IHttpContextAccessor contextAccessor, IMovieRepository movieRepository) : base(repository)
        {
            _contextAccessor = contextAccessor;
            _movieRepository = movieRepository;
        }

        public async Task<GetOrderQueryViewModel> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var id = EncryptionService.Decrypt(request.Id);
            await MustExistsCheckWithId(id);
            var customerId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault().Value;
            await CheckIfOrderIsBelongToCustomer(EncryptionService.Decrypt(customerId), id);
            var order = await Repository.Get(x => x.Id == id);
            var movie = await _movieRepository.Get(x => x.Id == order.MovieId);
            CreateOrderMovieDto movieDto = new()
            {
                Name = movie.Name,
                Price = movie.Price
            };
            GetOrderQueryViewModel response = new()
            {
                Id = request.Id,
                Movie = movieDto,
                OrderDate = order.OrderDate.ToString("dd/MM/yyyy")
            };
            return response;
        }
    }
}
