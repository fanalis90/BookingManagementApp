using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Account
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordDto>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(f => f.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
