using API.DTOs.Bookings;
using API.Utilities.Enum;
using FluentValidation;

namespace API.Utilities.Validations.Booking
{
    public class CreateBookingValidator : AbstractValidator<CreateBookingDto>
    {
        //validator untuk create booking
        public CreateBookingValidator()
        {
            RuleFor(b => b.StartDate)
                .NotEmpty();

            //tidak boleh kosong dan lebih besar dari startdate
            RuleFor(b => b.EndDate)
                .NotEmpty()
                .GreaterThan(b => b.StartDate);

       
            RuleFor(b => b.Status)
                .NotNull()
                .IsInEnum();

            RuleFor(b => b.Remarks)
                .NotEmpty();

            RuleFor(b => b.RoomGuid)
                .NotEmpty();

            RuleFor(b => b.EmployeeGuid)
                .NotEmpty();
        
        }
    }
}
