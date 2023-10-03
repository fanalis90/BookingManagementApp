using API.DTOs.Educations;
using API.DTOs.Employees;
using API.DTOs.Universities;
using API.Contracts;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using API.Utilities.Handlers;
using API.DTOs.Accounts;
using API.DTOs.Rooms;
using API.Utilities.Handlers.Exceptions;
using API.DTOs.AccountRoles;

namespace API.Controllers
{
    //membuat endpoint routing untuk employee controller 
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        //membuat employee repository untuk mengakses database sebagai readonly dan private
        private readonly IEmployeeRepository _employeeRepository;
        //dependency injection dilakukan
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        //method get dari http untuk getall universities
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _employeeRepository.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
            var data = result.Select(i => (EmployeeDto)i);
            return Ok(new ResponseOkHandler<IEnumerable<EmployeeDto>>(data));
        }
        //method get dari http untuk getByGuid employee
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _employeeRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            }
            return Ok(new ResponseOkHandler<EmployeeDto>((EmployeeDto)result));
        }
        //method post dari http untuk create employee
        [HttpPost]
        public IActionResult Create(CreateEmployeeDto createEmployeeDto)
        {
            try
            {
                Employee toCreate = createEmployeeDto;
                toCreate.NIK = GenerateNIKHandler.GenerateNIK(_employeeRepository);
                var result = _employeeRepository.Create(toCreate);
                return Ok(new ResponseOkHandler<EmployeeDto>((EmployeeDto)result));

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }
        }

        //method put dari http untuk Update employee
        [HttpPut]
        public IActionResult Update(EmployeeDto employeeDto)
        {
            try
            {
                var entity = _employeeRepository.GetByGuid(employeeDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Data Not Found"));

                }
                Employee toUpdate = employeeDto;
                toUpdate.CreatedDate = entity.CreatedDate;
                var result = _employeeRepository.Update(employeeDto);
                return Ok(new ResponseOkHandler<String>("Data Updated"));

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }

        }
        //method delete dari http untuk delete employee
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        { try { 
                var employee = _employeeRepository.GetByGuid(guid);
                if (employee is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Data Not Found"));

                }
                var result = _employeeRepository.Delete(employee);
                return Ok(new ResponseOkHandler<String>("Data Deleted"));
        }catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }

        }
    }
}
