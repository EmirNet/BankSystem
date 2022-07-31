using BankSystem.Application.Dtos;
using BankSystem.Application.Exceptions;
using BankSystem.Application.Services.Interfaces;
using BankSystem.Common;
using BankSystem.Common.Configurations;
using BankSystem.Domain.Entities;
using BankSystem.Persistence.Exceptions;
using BankSystem.Persistence.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BankSystem.Application.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IClientRepository _clientRepository;

        private readonly IOptions<AuthOptions> _authOptions;

        public AccountService(IClientRepository clientRepository, IOptions<AuthOptions> authOptions)
        {
            _clientRepository = clientRepository;
            _authOptions = authOptions;
        }

        public async Task<string> SignInAsync(SignInDto signInDto)
        {
            var client = await _clientRepository.GetClientByEmailAsync(signInDto.Email);

            if (client == null)
            {
                throw new EntityNotFoundException();
            }

            var userHashed = PasswordHasher.VerifyPassword(signInDto.Password, client.Password);

            if (!userHashed)
            {
                throw new InvalidPasswordOrEmailException();
            }

            return GenerateJwtToken(client);
        }

        public async Task<string> SignUpAsync(SignUpDto signUpDto)
        {
            var clientExist = await _clientRepository.GetClientByEmailAsync(signUpDto.Email);

            if (clientExist != null)
            {
                throw new LoginAlreadyExistException();
            }

            var passwordHashed = PasswordHasher.HashPassword(signUpDto.Password);

            var client = new Client
            {
                Firstname = signUpDto.Name,
                Email = signUpDto.Email,
                Password = passwordHashed
            };

            await _clientRepository.CreateClientAsync(client);

            return GenerateJwtToken(client);
        }

        private string GenerateJwtToken(Client client)
        {
            var authParams = _authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();

            var claims = new List<Claim>();

            if (client.Role != null)
            {
                claims.Add(new(ClaimTypes.Role, client.Role.Name));
            }

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.UtcNow.AddDays(authParams.TokenLifetime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
