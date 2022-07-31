using BankSystem.Domain.Entities;
using BankSystem.Persistence.Context;
using BankSystem.Persistence.Exceptions;
using BankSystem.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Persistence.Repositories.Implementations
{
    public class ClientRepository : IClientRepository
    {
        private readonly BankSystemContext _appDbContext;

        public ClientRepository(BankSystemContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //This pagination can also be done using switch case
        public IQueryable<Client> GetAllClients(string ascPrice, string descPrice, int pageNumber, int pageSize)
        {
            IQueryable<Client> clients = _appDbContext.Clients;

            if (!string.IsNullOrEmpty(ascPrice))
            {
                clients.OrderBy(x => x.Firstname);
            }

            if (!string.IsNullOrEmpty(descPrice))
            {
                clients.OrderByDescending(x => x.Firstname);
            }

            return clients.Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().AsQueryable();
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            var client = await _appDbContext.Clients.Include(x => x.Address).Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == id);

            if (client == null)
            {
                throw new EntityNotFoundException();
            }

            return client;
        }

        public async Task CreateClientAsync(Client client)
        {
            await _appDbContext.Clients.AddAsync(client);

            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateClientAsync(Client client)
        {
            _appDbContext.Clients.Update(client);

            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteClientAsync(int id)
        {
            var user = await _appDbContext.Clients.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException();
            }

            _appDbContext.Clients.Remove(user);

            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Client> GetClientByEmailAsync(string userLogin)
        {
            var client = await _appDbContext.Clients.FirstOrDefaultAsync(x => x.Email == userLogin);

            if (client == null)
            {
                throw new EntityNotFoundException();
            }

            return client;
        }
    }
}
