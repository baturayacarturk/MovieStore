using Application.Features.Actors.Business;
using Application.Features.Directors.Business;
using Application.Features.Movies.Business;
using Application.Features.Users.Business;
using Application.Services.Repositories;
using Core.Application.Pipelines.Logging;
using Core.CrossCuttingConcerns.Logging;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddLogging(builder=>builder.AddConsole());
            services.AddScoped<ActorUseCase>();
            services.AddScoped<DirectorUseCase>();
            services.AddScoped<MovieUseCase>();
            services.AddScoped<UserUseCase>();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<ConcreteLoggerService>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
            
            return services;
        }
    }
}
