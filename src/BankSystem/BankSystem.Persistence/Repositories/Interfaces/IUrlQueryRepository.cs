using BankSystem.Domain.Entities;

namespace BankSystem.Persistence.Repositories.Interfaces
{
    public interface IUrlQueryRepository
    {
        Task SaveUrlQueryParametersAsync(UrlPath urlQuery);
        IQueryable<UrlPath> GetLastThreeUrlQueryParametersAsync();
    }
}
