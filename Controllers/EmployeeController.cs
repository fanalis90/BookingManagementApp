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
        private readonly IEducationRepository _educationRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IAccountRepository _accountRepository;
        //dependency injection dilakukan
        public EmployeeController(IEmployeeRepository employeeRepository, IEducationRepository educationRepository, IUniversityRepository universityRepository, IAccountRepository accountRepository)
        {
            _employeeRepository = employeeRepository;
            _educationRepository = educationRepository;
            _universityRepository = universityRepository;
            _accountRepository = accountRepository;
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var employee = _employeeRepository.GetByEmail(loginDto.Email);
            if(employee is null)
            {
                return BadRequest(new ResponseBadRequestHandler("Email or Password is invalid"));
            }
            var account = _accountRepository.GetByGuid(employee.Guid);
            if (account is null)
            {
                return NotFound(new ResponseNotFoundHandler("Account Not Found"));
            }
            var isValidPassword = HashHandler.verifvyPassword(loginDto.Password, account.Password);
            if (!isValidPassword)
            {
                return BadRequest(new ResponseBadRequestHandler("Email or Password is invalid"));
            }
            return Ok(new ResponseOkHandler<string>("Login Success"));
        }
        
        [HttpPost("Register")]
        public IActionResult Register(RegisterDto registerDto)
        {
            var employee = _employeeRepository.GetByEmail(registerDto.Email);
            if(employee is null)
            {
                employee = registerDto;
                employee.NIK = GenerateNIKHandler.GenerateNIK(_employeeRepository.GetLastNik());
                employee = _employeeRepository.Create(employee);
            } else
            {
                return BadRequest(new ResponseBadRequestHandler("Email is Used"));
            }
            
            var university = _universityRepository.GetUniversityNameByCode(registerDto.UniversityCode);
            if (university is null)
            {
                university = _universityRepository.Create(registerDto);
            }

            if (registerDto.Password != registerDto.ConfirmPassword)
            { 
                return BadRequest(new ResponseBadRequestHandler("Password dont Match"));
            }

            var education = _educationRepository.GetByGuid(employee.Guid);
            if (education is null)
            {
                Education educationcreate = registerDto;
                educationcreate.Guid = employee.Guid;
                educationcreate.UniversityGuid = university.Guid;
                _educationRepository.Create(educationcreate);
            }
            Account account = registerDto;
            account.Guid = employee.Guid;
            account.Password = HashHandler.HashPassword(registerDto.Password);
            EmployeeDetailsDto responseRegister = (EmployeeDetailsDto) registerDto;
            responseRegister.Guid = employee.Guid;
            responseRegister.Nik = employee.NIK;
            responseRegister.University = university.Name;
            return Ok(new ResponseOkHandler<EmployeeDetailsDto>(responseRegister));
        }

        [HttpGet("details")]
        public IActionResult GetDetails()
        {
            var employee = _employeeRepository.GetAll();
            var education = _educationRepository.GetAll();
            var university = _universityRepository.GetAll();
            if(!(employee.Any() && education.Any() && university.Any()))
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
            var employeeDetails = from emp in employee
                                  join edu in education on emp.Guid equals edu.Guid
                                  join unv in university on edu.UniversityGuid equals unv.Guid
                                  select new EmployeeDetailsDto
                                  {
                                      Guid = emp.Guid,
                                      Nik = emp.NIK,
                                      FullName = string.Concat(emp.FirstName, " ", emp.LastName),
                                      BirthDate = emp.BirthDate,
                                      Gender = emp.Gender.ToString(),
                                      HiringDate = emp.HiringDate,
                                      Email = emp.Email,
                                      PhoneNumber = emp.PhoneNumber,
                                      Major = edu.Major,
                                      Degree = edu.Degree,
                                      Gpa = edu.GPA,
                                      University = unv.Name
                                  };

            return Ok(new ResponseOkHandler<IEnumerable<EmployeeDetailsDto>>(employeeDetails));
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
                toCreate.NIK = GenerateNIKHandler.GenerateNIK(_employeeRepository.GetLastNik());
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
