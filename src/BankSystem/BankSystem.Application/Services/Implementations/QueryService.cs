using BankSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BankSystem.Application.Services.Implementations
{
    public class QueryService : IQueryService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QueryService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetEndpoinsAsync()
        {
            return _httpContextAccessor.HttpContext.Request.Path;
        }
    }
}
