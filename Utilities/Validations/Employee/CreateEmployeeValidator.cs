using API.DTOs.Employees;
using API.Utilities.Enum;
using FluentValidation;

namespace API.Utilities.Validations.Employee
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeDto>
    {
        //membuat validator untuk create employee
        public CreateEmployeeValidator() {
            RuleFor(e => e.FirstName)
                .NotEmpty();

            RuleFor(e => e.BirthDate)
                .NotEmpty()
                .LessThan(DateTime.Now.AddYears(-18));
            //notnull agar bisa isi 0
            RuleFor(e => e.Gender)
                .NotNull()
                .IsInEnum();

            RuleFor(e => e.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(e => e.PhoneNumber)
               .NotEmpty()
               .MinimumLength(9)
               .MaximumLength(20);
        }

    }
}
