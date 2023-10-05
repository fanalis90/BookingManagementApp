using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Account
{
    //membuat validasi untuk createAccount
    public class CreateAccountValidator : AbstractValidator<CreateAccountDto>
    {

        public CreateAccountValidator()
        {
            RuleFor(a => a.Guid)
                .NotEmpty();

            RuleFor(a => a.Otp)
                .NotNull();

            RuleFor(a => a.ExpiredTime)
                .NotEmpty();

            //menetapkan minimum dan maksimum password beserta syaratnya
            RuleFor(a => a.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches("[A-Z]")
                .Matches("[a-z]")
                .Matches("[!@#$%^&*()_+\\-=\\[\\]{};':\",.<>?]");
            //
            RuleFor(a => a.IsUsed)
                .NotEmpty();


        }
    }
}
