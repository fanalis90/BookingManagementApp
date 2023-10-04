using API.DTOs.Educations;
using FluentValidation;

namespace API.Utilities.Validations.Education
{
    public class CreateEducationValidator : AbstractValidator<CreateEducationDto>
    {
        
        public CreateEducationValidator()
        {
            RuleFor(e => e.Guid)
                .NotEmpty();

            RuleFor(e => e.Major)
                .NotEmpty(); 
            
            RuleFor(e => e.Degree)
                .NotEmpty(); 
            
            RuleFor(e => e.Gpa)
                .NotEmpty()
                .Must(gpa => gpa >= 0.0f && gpa <= 4.0f);

            RuleFor(e => e.UniversityGuid)
                .NotEmpty();
        }
    }
}
