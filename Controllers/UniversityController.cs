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

            return Ok(result);
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
            return Ok(result);
        }
        //method post dari http untuk create university
        [HttpPost]
        public IActionResult Create(University university)
        {
            var result = _universityRepository.Create(university);
            if( result  is null)
            {
                return BadRequest("Failed To Create Data");
            }
            return Ok(result);
        }

        //method put dari http untuk Update university
        [HttpPut]
        public IActionResult Update(University university)
        {
            var result = _universityRepository.Update(university);
            if (!result)
            {
                return BadRequest("Failed To Update Data");
            }

            return Ok(result);
        }
        //method delete dari http untuk delete university
        [HttpDelete]
        public IActionResult Delete(Guid guid) {
            var university = _universityRepository.GetByGuid(guid);
            var result = _universityRepository.Delete(university);
            if (!result)
            {
                return BadRequest("Failed To Delete Data");
            }

            return Ok(result);
        }
    }
}
