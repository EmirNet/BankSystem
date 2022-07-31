using BankSystem.Application.Dtos;

namespace BankSystem.Application.Services.Interfaces
{
    public interface IUrlQueryService
    {
        IQueryable<UrlQueryDto> GetLastThreeUrlQueryParametersAsync();
    }
}
