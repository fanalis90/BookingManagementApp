using API.DTOs.Educations;
using FluentValidation;

namespace API.Utilities.Validations.Education
{
    public class UpdateEducationValidator : AbstractValidator<EducationDto>
    {
        //validation untuk update education
        public UpdateEducationValidator()
        {
            RuleFor(e => e.Guid)
            .NotEmpty();

            RuleFor(e => e.Major)
                .NotEmpty();

            RuleFor(e => e.Degree)
                .NotEmpty();

            //membuat aturan gpa lebih dari 0.0 dan krang dr 4.0
            RuleFor(e => e.Gpa)
                .NotEmpty()
                .Must(gpa => gpa >= 0.0f && gpa <= 4.0f);

            RuleFor(e => e.UniversityGuid)
                .NotEmpty();
        }
    }
}
