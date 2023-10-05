﻿using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Account
{
    public class UpdateAccountValidator : AbstractValidator<AccountDto>
    {
        public UpdateAccountValidator()
        {
            RuleFor(a => a.Guid)
                .NotEmpty();

            RuleFor(a => a.Otp)
                .NotEmpty();

            RuleFor(a => a.ExpiredTime)
                .NotEmpty();

            //menetapkan minimum dan maksimum password beserta syaratnya
            RuleFor(a => a.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches("[A-Z]")
                .Matches("[a-z]")
                .Matches("[0-9]")
                .Matches("[!@#$%^&*()_+\\-=\\[\\]{};':\",.<>?]");
            //
            RuleFor(a => a.IsUsed)
                .NotEmpty();


        }
    }
}
