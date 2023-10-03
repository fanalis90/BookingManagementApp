using API.DTOs.Bookings;
using BookingManagementApp.Contracts;
using BookingManagementApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagementApp.Controllers
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
                return NotFound("Data Not Found");
            }
            var data = result.Select(i => (BookingDto)i);

            return Ok(data);
        }
        //method get dari http untuk getByGuid booking
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _bookingRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Data Not Found");

            }
            return Ok((BookingDto) result);
        }
        //method post dari http untuk create booking
        [HttpPost]
        public IActionResult Create(CreateBookingDto booking)
        {
            var result = _bookingRepository.Create(booking);
            if (result is null)
            {
                return BadRequest("Failed To Create Data");
            }
            return Ok((BookingDto) result);
        }

        //method put dari http untuk Update booking
        [HttpPut]
        public IActionResult Update(BookingDto booking)
        {
            var result = _bookingRepository.Update(booking);
            if (!result)
            {
                return BadRequest("Failed To Update Data");
            }

            return Ok(result);
        }
        //method delete dari http untuk delete booking
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var booking = _bookingRepository.GetByGuid(guid);
            var result = _bookingRepository.Delete(booking);
            if (!result)
            {
                return BadRequest("Failed To Delete Data");
            }

            return Ok(result);
        }
    }
}
