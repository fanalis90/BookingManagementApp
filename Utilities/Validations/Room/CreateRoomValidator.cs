using API.DTOs.Rooms;
using FluentValidation;

namespace API.Utilities.Validations.Room
{
    public class CreateRoomValidator : AbstractValidator<CreateRoomDto>
    {
        public CreateRoomValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(r => r.Floor)
                .NotEmpty();

            RuleFor(r => r.Capacity)
                .NotEmpty();
        }

    }
}
