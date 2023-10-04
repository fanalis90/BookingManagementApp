using API.DTOs.Employees;
using FluentValidation;

namespace API.Utilities.Validations.Employee
{
    public class UpdateEmployeeValidator : AbstractValidator<EmployeeDto>
    {
        //validation untuk employee update
        public UpdateEmployeeValidator()
        {
            RuleFor(e => e.Guid).NotEmpty();

            RuleFor(e => e.FirstName)
               .NotEmpty();

            RuleFor(e => e.BirthDate)
                .NotEmpty()
                .LessThan(DateTime.Now.AddYears(-18));

            //not null supaya boleh mengisi 0
            RuleFor(e => e.Gender)
                .NotNull()
                .IsInEnum();

            RuleFor(e => e.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(e => e.PhoneNumber)
               .NotEmpty()
               .MinimumLength(9)
               .MinimumLength(20);
        }
    }
}
