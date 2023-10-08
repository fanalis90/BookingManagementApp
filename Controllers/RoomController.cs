using API.DTOs.Educations;
using API.DTOs.Rooms;
using API.DTOs.Universities;
using API.Contracts;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using API.DTOs.Accounts;
using API.Utilities.Handlers;
using API.Utilities.Handlers.Exceptions;
using API.DTOs.AccountRoles;
using API.DTOs.Bookings;

namespace API.Controllers
{
    //membuat endpoint routing untuk room controller 
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        //membuat room repository untuk mengakses database sebagai readonly dan private
        private readonly IRoomRepository _roomRepository;
        //dependency injection dilakukan
        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

      


        //method get dari http untuk getall universities
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _roomRepository.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
            var data = result.Select(i => (RoomDto)i);
            return Ok(new ResponseOkHandler<IEnumerable<RoomDto>>(data));
        }
        //method get dari http untuk getByGuid room
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _roomRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            }
            return Ok(new ResponseOkHandler<RoomDto>((RoomDto)result));
        }
        //method post dari http untuk create room
        [HttpPost]
        public IActionResult Create(CreateRoomDto createRoomDto)
        {
            try
            {
                Room toCreate = createRoomDto;
                var result = _roomRepository.Create(toCreate);
                return Ok(new ResponseOkHandler<RoomDto>((RoomDto)result));

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }
        }

        //method put dari http untuk Update room
        [HttpPut]
        public IActionResult Update(RoomDto roomDto)
        {
            try
            {
                var entity = _roomRepository.GetByGuid(roomDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Data Not Found"));

                }
                Room toUpdate = roomDto;
                toUpdate.CreatedDate = entity.CreatedDate;
                var result = _roomRepository.Update(roomDto);
                return Ok(new ResponseOkHandler<String>("Data Updated"));

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }

        }
        //method delete dari http untuk delete room
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var room = _roomRepository.GetByGuid(guid);
                if (room is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Data Not Found"));

                }
                var result = _roomRepository.Delete(room);
                return Ok(new ResponseOkHandler<String>("Data Deleted"));

            } catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }
            
        }
    }
}
