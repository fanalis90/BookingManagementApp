using API.DTOs.Roles;
using FluentValidation;

namespace API.Utilities.Validations.Role
{
    public class UpdateRoleValidator : AbstractValidator<RoleDto>
    {
        //validation untuk update role
        public UpdateRoleValidator()
        {
            RuleFor(r => r.Guid)
                .NotEmpty();

            RuleFor(r => r.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
