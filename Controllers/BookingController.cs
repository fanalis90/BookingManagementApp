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
        private readonly IRoomRepository _roomRepository;
        private readonly IEmployeeRepository _employeeRepository;
        //dependency injection dilakukan
        public BookıngController(IBookingRepository bookingRepository, IRoomRepository roomRepository, IEmployeeRepository employeeRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _employeeRepository = employeeRepository;
        }
        [HttpGet("Booking-length")]
        public IActionResult GetBookingLength()
        {
            var booking = _bookingRepository.GetAll();
            var room = _roomRepository.GetAll();
            if (!room.Any() && !booking.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
            var bookingLength = from b in booking
                                join r in room on b.RoomGuid equals r.Guid
                                select new BookingLengthDto
                                {
                                    RoomGuid = r.Guid,
                                    RoomName = r.Name,
                                    //menggunakan handler untuk menghitung panjang dari booking
                                    BookingLength = CalculateBookingLengthHandler.CalculateLength(b.StartDate, b.EndDate),
                                };

            return Ok(new ResponseOkHandler<IEnumerable<BookingLengthDto>>(bookingLength));
        }


        [HttpGet("Available-rooms")]
        public IActionResult GetAvailableRoom() {
            //mengambil data berdasarkan roomguid
            var bookingRoomGuid = _bookingRepository.GetAll().Select(b => b.RoomGuid).ToList();
            //mengambul data room berdasarkan yang tidak ada dalam booking
            var room = _roomRepository.GetAll().Where(r => !bookingRoomGuid.Contains(r.Guid))
                       .Select( r =>  new AvailableRoomDto
                       {
                           RoomGuid = r.Guid,
                           RoomName = r.Name,
                           Capacity = r.Capacity,
                           Floor = r.Floor,
                       });
            if (!room.Any())
            {
                return NotFound(new ResponseNotFoundHandler("No Available Rooms"));
            }
            return Ok(new ResponseOkHandler<IEnumerable<AvailableRoomDto>>(room));
        }

        [HttpGet("booked-rooms")]
        public IActionResult GetBookedRoom()
        {
            //melakukan join 3 collection untuk menampilkan data yang di inginkan
            var bookedRoomDetail = from b in _bookingRepository.GetAll()
                                   join r in _roomRepository.GetAll() on b.RoomGuid equals r.Guid
                                   join e in _employeeRepository.GetAll() on b.EmployeeGuid equals e.Guid
                                   select new GetBookedRoomDto
                                   {
                                       BookingGuid = b.Guid,
                                       RoomName = r.Name,
                                       Status = b.Status.ToString(),
                                       Floor = r.Floor,
                                       BookedBy = e.FirstName + " " + e.LastName,
                                   };
            if (!bookedRoomDetail.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
            return Ok(new ResponseOkHandler<IEnumerable<GetBookedRoomDto>>(bookedRoomDetail));

        }

        [HttpGet("Details")]
        public IActionResult GetDetails()
        {
            //menggunakan get all untuk mendapat collection dari 3 tabel untuk menampilkan data yang di inginkan
            var bookingDetails = from b in _bookingRepository.GetAll()
                                 join r in _roomRepository.GetAll() on b.RoomGuid equals r.Guid
                                 join e in _employeeRepository.GetAll() on b.EmployeeGuid equals e.Guid
                                 select new BookingDetailsDto
                                 {
                                     Guid = b.Guid,
                                     BookedNIK = e.NIK,
                                     BookedBy = e.FirstName + " " + e.LastName,
                                     RoomName = r.Name,
                                     StartDate = b.StartDate,
                                     EndDate = b.EndDate,
                                     Status = b.Status.ToString(),
                                     Remarks = b.Remarks,

                                 };
            if (!bookingDetails.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
            return Ok(new ResponseOkHandler<IEnumerable<BookingDetailsDto>>(bookingDetails));
        }

        [HttpGet("Details/{Guid}")]
        public IActionResult GetDetailsById(Guid Guid)
        {
            //mengambil data berdasarkan guid booking
            var booking = _bookingRepository.GetByGuid(Guid);
            if (booking is null)
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
            var employee = _employeeRepository.GetByGuid(booking.EmployeeGuid);
            var room = _roomRepository.GetByGuid(booking.RoomGuid);
            var bookingDetail = new BookingDetailsDto
            {
                Guid = booking.Guid,
                BookedNIK = employee.NIK,
                BookedBy = employee.FirstName + " " + employee.LastName,
                RoomName = room.Name,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                Status = booking.Status.ToString(),
                Remarks = booking.Remarks,

            };
            return Ok(new ResponseOkHandler<BookingDetailsDto>(bookingDetail));
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
