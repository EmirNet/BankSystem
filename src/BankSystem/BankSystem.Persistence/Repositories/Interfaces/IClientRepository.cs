using BankSystem.Domain.Entities;

namespace BankSystem.Persistence.Repositories.Interfaces
{
    public interface IClientRepository
    {
        IQueryable<Client> GetAllClients(string ascPrice, string descPrice, int pageNumber, int pageSize);

        Task<Client> GetClientByIdAsync(int id);

        Task CreateClientAsync(Client client);

        Task UpdateClientAsync(Client client);

        Task DeleteClientAsync(int id);
        Task<Client> GetClientByEmailAsync(string clientLogin);
    }
}
