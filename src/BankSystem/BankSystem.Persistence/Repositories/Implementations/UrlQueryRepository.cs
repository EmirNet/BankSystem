using BankSystem.Domain.Entities;
using BankSystem.Persistence.Context;
using BankSystem.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Persistence.Repositories.Implementations
{
    public class UrlQueryRepository : IUrlQueryRepository
    {

        private readonly BankSystemContext _bankSystemContext;

        public UrlQueryRepository(BankSystemContext bankSystemContext)
        {
            _bankSystemContext = bankSystemContext;
        }

        public IQueryable<UrlPath> GetLastThreeUrlQueryParametersAsync()
        {
            return _bankSystemContext.UrlPaths.OrderByDescending(x => x.Id).Take(3).AsNoTracking().AsQueryable();
        }

        public async Task SaveUrlQueryParametersAsync(UrlPath urlQuery)
        {
            await _bankSystemContext.UrlPaths.AddAsync(urlQuery);

            await _bankSystemContext.SaveChangesAsync();
        }
    }
}
