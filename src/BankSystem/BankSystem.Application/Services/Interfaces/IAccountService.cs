using BankSystem.Application.Dtos;

namespace BankSystem.Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<string> SignUpAsync(SignUpDto signUpDto);
        Task<string> SignInAsync(SignInDto signInDto);
    }
}
