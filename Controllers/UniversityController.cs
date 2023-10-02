using API.DTOs.Universities;
using BookingManagementApp.Contracts;
using BookingManagementApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagementApp.Controllers
{
    //membuat endpoint routing untuk university controller 
    [ApiController]
    [Route("api/[controller]")]
    public class UniversityController : ControllerBase
    {
        //membuat university repository untuk mengakses database sebagai readonly dan private
        private readonly IUniversityRepository _universityRepository;
        //dependency injection dilakukan
        public UniversityController(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }
        //method get dari http untuk getall universities
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _universityRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }
            var data = result.Select(u => (UniversityDto) u);
            /*var universityDto = new List<UniversityDto>();
               foreach (var university in result)
               {
                   universityDto.Add((UniversityDto) university);
               }*/
            return Ok(data);
        }
        //method get dari http untuk getByGuid university
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _universityRepository.GetByGuid(guid);
            if(result is null)
            {
                return NotFound("Data Not Found");

            }
            return Ok((UniversityDto) result);
        }
        //method post dari http untuk create university
        [HttpPost]
        public IActionResult Create(CreateUniversityDto createUniversityDto)
        {
            var result = _universityRepository.Create(createUniversityDto);
            if( result  is null)
            {
                return BadRequest("Failed To Create Data");
            }
            return Ok((UniversityDto) result);
        }

        //method put dari http untuk Update university
        [HttpPut]
        public IActionResult Update(UniversityDto universityDto)
        {
            var entity = _universityRepository.GetByGuid(universityDto.Guid);
            if (entity is null)
            {
                return NotFound("Data Not Found");

            }
            University toUpdate = universityDto;
            toUpdate.CreatedDate = entity.CreatedDate;

            var result = _universityRepository.Update(toUpdate);
            if (!result)
            {
                return BadRequest("Failed To Update Data");
            }

            return Ok(result);
        }
        //method delete dari http untuk delete university
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid) {
            var university = _universityRepository.GetByGuid(guid);
            if (university is null)
            {
                return NotFound("Data Not Found");

            }
            var result = _universityRepository.Delete(university);
            if (!result)
            {
                return BadRequest("Failed To Delete Data");
            }

            return Ok(result);
        }
    }
}
