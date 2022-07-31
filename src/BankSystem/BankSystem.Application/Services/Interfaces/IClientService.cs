using BankSystem.Application.Dtos;

namespace BankSystem.Application.Services.Interfaces
{
    public interface IClientService
    {
        IQueryable<ClientDto> GetAllClients(string? ascPrice, string? descPrice, int pageNumber, int pageSize);
        Task<ClientDto> GetClientByIdAsync(int id);
        Task CreateClientAsync(CreateClientDto client);
        Task UpdateClientAsync(UpdateClientDto client);
        Task DeleteClientAsync(int id);
    }
}
