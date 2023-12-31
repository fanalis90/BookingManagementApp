﻿using API.DTOs.Universities;
using FluentValidation;

namespace API.Utilities.Validations.University
{
    public class CreateUniversityValidator : AbstractValidator<CreateUniversityDto>
    {
        //validation untuk create univeersity
        public CreateUniversityValidator()
        {
            RuleFor(u => u.Code)
                .NotEmpty();

            RuleFor(u => u.Name)
                .NotEmpty();
        }

    }
}
