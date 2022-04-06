using CleanArchMvc.Domain.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CleanArchMvc.WebUI.Extensions
{
    public class CustomErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainExceptionValidation)
            {
                context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            }
            catch (Exception)
            {
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            }
        }
    }
}