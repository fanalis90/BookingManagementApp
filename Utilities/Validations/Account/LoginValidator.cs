using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Account
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator() {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(a => a.Password)
              .NotEmpty()
              .MinimumLength(8)
              .Matches("[A-Z]")
              .Matches("[a-z]")
              .Matches("[0-9]")
              .Matches("[!@#$%^&*()_+\\-=\\[\\]{};':\",.<>?]");
        }
    }
}
