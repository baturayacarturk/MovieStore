using Core.CrossCuttingConcerns.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            if (ex.GetType() == typeof(ValidationException)) return CreateValidationException(context, ex);
            if (ex.GetType() == typeof(BusinessException)) return CreateBusinessException(context, ex);
            if (ex.GetType() == typeof(AuthorizationException)) return CreateAuthorizationException(context, ex);

            return CreateInternalException(context, ex);

        }

        private Task CreateInternalException(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
            LogInternalException(ex);
            return context.Response.WriteAsync(new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = context.Request.Path.HasValue ? context.Request.Path.ToString() : "Baturay Error",
                Title = "Internal exception",
                Detail = ex.Message,
                Instance = ""
            }.ToString());
        }

        private Task CreateBusinessException(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
            LogBusinessException(ex);
            return context.Response.WriteAsync(new BusinessProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = context.Request.Path.HasValue ? context.Request.Path.ToString() : "Baturay Error",
                Title = "Business exception",
                Detail = ex.Message,
                Instance = ""
            }.ToString());
        }

        private Task CreateValidationException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
            object errors = ((ValidationException)exception).ValidationResult;
            LogValidationException(exception);
            return context.Response.WriteAsync(new ValidationProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = context.Request.Path.HasValue ? context.Request.Path.ToString() : "Baturay Error",
                Title = "Validation error(s)",
                Detail = "",
                Instance = "",
                Errors = errors
            }.ToString());
        }

        private Task CreateAuthorizationException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.Unauthorized);
            LogAuthorizationException(exception);
            return context.Response.WriteAsync(new AuthorizationProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Type = context.Request.Path.HasValue ? context.Request.Path.ToString() : "Baturay Error",
                Title = "Authorization exception",
                Detail = exception.Message,
                Instance = ""
            }.ToString());
        }
        private void LogValidationException(Exception ex)
        {
            var logDetail = new LogDetailWithException
            {
                FullName = ex.TargetSite?.DeclaringType?.FullName,
                MethodName = ex.TargetSite?.Name,
                User = "N/A",
                Parameters = null, 
                ExceptionMessage = ex.Message
            };

            _logger.LogError(JsonConvert.SerializeObject(logDetail));
        }

        private void LogBusinessException(Exception ex)
        {
            var logDetail = new LogDetailWithException
            {
                FullName = ex.TargetSite?.DeclaringType?.FullName,
                MethodName = ex.TargetSite?.Name,
                User = "N/A",
                Parameters = null, 
                ExceptionMessage = ex.Message
            };

            _logger.LogError(JsonConvert.SerializeObject(logDetail));
        }

        private void LogAuthorizationException(Exception ex)
        {
            var logDetail = new LogDetailWithException
            {
                FullName = ex.TargetSite?.DeclaringType?.FullName,
                MethodName = ex.TargetSite?.Name,
                User = "N/A",
                Parameters = null, 
                ExceptionMessage = ex.Message
            };

            _logger.LogError(JsonConvert.SerializeObject(logDetail));
        }

        private void LogInternalException(Exception ex)
        {
            var logDetail = new LogDetailWithException
            {
                FullName = ex.TargetSite?.DeclaringType?.FullName,
                MethodName = ex.TargetSite?.Name,
                User = "N/A",
                Parameters = null, 
                ExceptionMessage = ex.Message
            };

            _logger.LogError(JsonConvert.SerializeObject(logDetail));
        }

    }
}
