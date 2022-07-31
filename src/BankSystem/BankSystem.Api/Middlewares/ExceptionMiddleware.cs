using BankSystem.Application.Exceptions;
using BankSystem.Common.Errors;
using BankSystem.Persistence.Exceptions;
using System.Net;

namespace BankSystem.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (EntityNotFoundException)
            {

                httpContext.Response.ContentType = "application/json";

                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

                await httpContext.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = "Entity Not Found"
                }.ToString());
            }

            catch (LoginAlreadyExistException)
            {

                httpContext.Response.ContentType = "application/json";

                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

                await httpContext.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = "User with this login already exists"
                }.ToString());
            }

            catch (InvalidPasswordOrEmailException)
            {
                httpContext.Response.ContentType = "application/json";

                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

                await httpContext.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = "Invalid password or login"
                }.ToString());
            }

            catch (Exception ex)
            {

                httpContext.Response.ContentType = "application/json";

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await httpContext.Response.WriteAsync(ex.Message);
            }
        }
    }
}
