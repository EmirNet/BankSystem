using BankSystem.Application.Dtos;
using BankSystem.Application.Services.Interfaces;
using BankSystem.Domain.Entities;
using BankSystem.Persistence.Repositories.Interfaces;

namespace BankSystem.Application.Services.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IQueryService _queryService;
        private readonly IUrlQueryRepository _urlQueryRepository;

        public ClientService(
            IClientRepository clientRepository,
            IQueryService queryService,
            IUrlQueryRepository urlQueryRepository)
        {
            _clientRepository = clientRepository;
            _queryService = queryService;
            _urlQueryRepository = urlQueryRepository;
        }

        public IQueryable<ClientDto> GetAllClients(string? ascPrice, string? descPrice, int pageNumber, int pageSize)
        {
            var clients = _clientRepository.GetAllClients(ascPrice, descPrice, pageNumber, pageSize);

            string path = _queryService.GetEndpoinsAsync();

            _urlQueryRepository.SaveUrlQueryParametersAsync(new UrlPath { Path = path }).GetAwaiter().GetResult();

            return clients.Select(x => new ClientDto
            {
                Id = x.Id,
                Firstname = x.Firstname,
                Lastname = x.Lastname,
                Email = x.Email,
                Password = x.Password,
                PersonalId = x.PersonalId,
                MobileNumber = x.MobileNumber,
                Sex = x.Sex,
                Account = x.Account,
                AddressDto = new AddressDto
                {
                    Id = x.Address.Id,
                    City = x.Address.City,
                    Country = x.Address.Country,
                    Street = x.Address.Street,
                    Zipcode = x.Address.Zipcode
                },
                RoleDto = new RoleDto
                {
                    Id = x.Role.Id,
                    Name = x.Role.Name,
                }
            });


        }

        public async Task<ClientDto> GetClientByIdAsync(int id)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);

            if (client.Address == null)
            {
                client.Address = new Address();
            }

            if (client.Role == null)
            {
                client.Role = new Role();
            }

            return new ClientDto
            {
                Id = client.Id,
                Firstname = client.Firstname,
                Lastname = client.Lastname,
                Email = client.Email,
                Password = client.Password,
                PersonalId = client.PersonalId,
                MobileNumber = client.MobileNumber,
                Sex = client.Sex,
                Account = client.Account,
                AddressDto = new AddressDto
                {
                    Id = client.Address.Id,
                    City = client.Address.City,
                    Country = client.Address.Country,
                    Street = client.Address.Street,
                    Zipcode = client.Address.Zipcode
                },
                RoleDto = new RoleDto
                {
                    Id = client.Role.Id,
                    Name = client.Role.Name,
                }
            };
        }

        public async Task CreateClientAsync(CreateClientDto createClientDto)
        {
            await _clientRepository.CreateClientAsync(new Client
            {
                Firstname = createClientDto.Firstname,
                Lastname = createClientDto.Lastname,
                Email = createClientDto.Email,
                Password = createClientDto.Password,
                PersonalId = createClientDto.PersonalId,
                MobileNumber = createClientDto.MobileNumber,
                Account = createClientDto.Account,
                Sex = createClientDto.Sex,
                Address = new Address
                {
                    Id = createClientDto.AddressDto.Id,
                    City = createClientDto.AddressDto.City,
                    Country = createClientDto.AddressDto.Country,
                    Street = createClientDto.AddressDto.Street,
                    Zipcode = createClientDto.AddressDto.Zipcode
                },
                Role = new Role
                {
                    Id = createClientDto.RoleDto.Id,
                    Name = createClientDto.RoleDto.Name,
                }
            });
        }

        public async Task UpdateClientAsync(UpdateClientDto updateClientDto)
        {
            await _clientRepository.UpdateClientAsync(new Client
            {
                Id = updateClientDto.Id,
                Firstname = updateClientDto.Firstname,
                Lastname = updateClientDto.Lastname,
                Account = updateClientDto.Account,
                Email = updateClientDto.Email,
                Password = updateClientDto.Password,
                PersonalId = updateClientDto.PersonalId,
                MobileNumber = updateClientDto.MobileNumber,
                Sex = updateClientDto.Sex,
                Address = new Address
                {
                    Id = updateClientDto.AddressDto.Id,
                    City = updateClientDto.AddressDto.City,
                    Country = updateClientDto.AddressDto.Country,
                    Street = updateClientDto.AddressDto.Street,
                    Zipcode = updateClientDto.AddressDto.Zipcode
                },
                Role = new Role
                {
                    Id = updateClientDto.RoleDto.Id,
                    Name = updateClientDto.RoleDto.Name,
                }
            });
        }

        public async Task DeleteClientAsync(int id)
        {
            await _clientRepository.DeleteClientAsync(id);
        }
    }
}
