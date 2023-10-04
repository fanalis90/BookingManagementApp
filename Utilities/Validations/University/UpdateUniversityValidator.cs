using API.DTOs.Universities;
using FluentValidation;

namespace API.Utilities.Validations.University
{
    public class UpdateUniversityValidator : AbstractValidator<UniversityDto>
    {
        //validation untuk update university
        public UpdateUniversityValidator()
        {
            RuleFor(u => u.Guid)
                .NotEmpty();

            RuleFor(u => u.Code)
                .NotEmpty();

            RuleFor(u => u.Name)
                .NotEmpty();
        }
    }
}
