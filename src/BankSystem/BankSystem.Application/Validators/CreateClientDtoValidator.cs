using BankSystem.Application.Dtos;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BankSystem.Application.Validators
{
    //I validate one entity as an example
    public class CreateClientDtoValidator : AbstractValidator<CreateClientDto>
    {
        public CreateClientDtoValidator()
        {
            RuleFor(x => x.Email).EmailAddress();

            RuleFor(x => x.Firstname).NotEmpty().NotNull().MaximumLength(60);

            RuleFor(x => x.Lastname).NotEmpty().NotNull().MaximumLength(60);

            //Example +33298765432
            RuleFor(x => x.MobileNumber).NotEmpty()
               .NotNull().WithMessage("Phone Number is required.")
               .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
               .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
               .Matches(new Regex(@"^([\+]?33[-]?|[0])?[1-9][0-9]{8}$")).WithMessage("PhoneNumber not valid");

            RuleFor(x => x.PersonalId).NotNull().Equal(11);

            RuleFor(x => x.Sex).NotNull().NotEmpty();

            RuleFor(x => x.Account).NotNull().When(x => x.Account > 1);

        }
    }
}
