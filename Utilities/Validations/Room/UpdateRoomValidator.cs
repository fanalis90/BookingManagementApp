using API.DTOs.Rooms;
using FluentValidation;

namespace API.Utilities.Validations.Room
{
    public class UpdateRoomValidator : AbstractValidator<RoomDto>
    {
        //validation untuk update room
        public UpdateRoomValidator()
        {
            RuleFor(r => r.Guid)
                .NotEmpty();

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
