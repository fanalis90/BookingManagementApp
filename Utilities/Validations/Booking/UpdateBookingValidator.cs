using API.DTOs.Bookings;
using FluentValidation;

namespace API.Utilities.Validations.Booking
{
    public class UpdateBookingValidator : AbstractValidator<BookingDto>
    {
        //membuat validator untuk update booking
        public UpdateBookingValidator()
        {
            RuleFor(b => b.Guid)
                .NotEmpty();

            RuleFor(b => b.StartDate)
               .NotEmpty();

            //tidak boleh kosong dan lebih besar dari startdate
            RuleFor(b => b.EndDate)
                .NotEmpty()
                .GreaterThan(b => b.StartDate);


            RuleFor(b => b.Status)
                .NotEmpty()
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
