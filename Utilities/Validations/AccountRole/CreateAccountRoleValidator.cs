using API.DTOs.AccountRoles;
using FluentValidation;

namespace API.Utilities.Validations.AccountRole
{
    public class CreateAccountRoleValidator : AbstractValidator<CreateAccountRoleDto>
    {
  
        //membuat validator untuk create account role
        public CreateAccountRoleValidator()
        {

            RuleFor(a => a.AccountGuid)
                .NotEmpty();

            RuleFor(a => a.RoleGuid)
                .NotEmpty();


        }
    }
}
