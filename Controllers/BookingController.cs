using API.DTOs.Bookings;
using API.DTOs.Universities;
using API.Contracts;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using API.DTOs.Accounts;
using API.Utilities.Handlers;
using API.Utilities.Handlers.Exceptions;
using API.DTOs.AccountRoles;

namespace API.Controllers
{
    //membuat endpoint routing untuk booking controller 
    [ApiController]
    [Route("api/[controller]")]
    public class BookıngController : ControllerBase
    {
        //membuat booking repository untuk mengakses database sebagai readonly dan private
        private readonly IBookingRepository _bookingRepository;
        //dependency injection dilakukan
        public BookıngController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        //method get dari http untuk getall universities
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _bookingRepository.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
            var data = result.Select(i => (BookingDto)i);

            return Ok(new ResponseOkHandler<IEnumerable<BookingDto>>(data));
        }
        //method get dari http untuk getByGuid booking
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _bookingRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            }
            return Ok(new ResponseOkHandler<BookingDto>((BookingDto)result));
        }
        //method post dari http untuk create booking
        [HttpPost]
        public IActionResult Create(CreateBookingDto createBookingDto)
        {
            try
            {
                Booking toCreate = createBookingDto;
                var result = _bookingRepository.Create(toCreate);
                return Ok(new ResponseOkHandler<BookingDto>((BookingDto)result));

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }
        }

        //method put dari http untuk Update booking
        [HttpPut]
        public IActionResult Update(BookingDto bookingDto)
        {
            try
            {
                var entity = _bookingRepository.GetByGuid(bookingDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Data Not Found"));

                }
                Booking toUpdate = bookingDto;
                toUpdate.CreatedDate = entity.CreatedDate;
                var result = _bookingRepository.Update(bookingDto);
                return Ok(new ResponseOkHandler<String>("Data Updated"));

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }

        }
        //method delete dari http untuk delete booking
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var booking = _bookingRepository.GetByGuid(guid);
                if (booking is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Data Not Found"));

                }
                var result = _bookingRepository.Delete(booking);
                return Ok(new ResponseOkHandler<String>("Data Deleted"));

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }

        }
    }
}
