using BankSystem.Application.Dtos;
using BankSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]

    //CRUD with client with sort, pagination
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }


        [HttpGet]
        public IQueryable<ClientDto> GetAllClientsAsync(string? ascPrice, string? descPrice, int pageNumber = 1, int pageSize = 5)
        {
            var clients = _clientService.GetAllClients(ascPrice, descPrice, pageNumber, pageSize);
            return clients;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDto>> GetClientByIdAsync(int id)
        {
            return Ok(await _clientService.GetClientByIdAsync(id));
        }

        [HttpPost]
        public async Task CreateClientAsync(CreateClientDto createClientDto)
        {
            await _clientService.CreateClientAsync(createClientDto);
        }

        [HttpPut]
        public async Task UpdateClientAsync(UpdateClientDto updateClientDto)
        {
            await _clientService.UpdateClientAsync(updateClientDto);
        }

        [HttpDelete]
        public async Task DeleteClientAsync(int id)
        {
            await _clientService.DeleteClientAsync(id);
        }
    }
}
