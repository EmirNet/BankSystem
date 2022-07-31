using BankSystem.Application.Dtos;
using BankSystem.Application.Services.Interfaces;
using BankSystem.Persistence.Repositories.Interfaces;

namespace BankSystem.Application.Services.Implementations
{
    public class UrlQueryService : IUrlQueryService
    {
        private readonly IUrlQueryRepository _urlQueryRepository;

        public UrlQueryService(IUrlQueryRepository urlQueryRepository)
        {
            _urlQueryRepository = urlQueryRepository;
        }

        public IQueryable<UrlQueryDto> GetLastThreeUrlQueryParametersAsync()
        {
            var urls = _urlQueryRepository.GetLastThreeUrlQueryParametersAsync();

            return urls.Select(x => new UrlQueryDto { Id = x.Id, Path = x.Path });
        }
    }
}
