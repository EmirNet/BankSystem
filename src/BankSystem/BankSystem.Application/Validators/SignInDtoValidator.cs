using BankSystem.Application.Dtos;
using FluentValidation;

namespace BankSystem.Application.Validators
{
    public class SignInDtoValidator : AbstractValidator<SignInDto>
    {
        public SignInDtoValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
