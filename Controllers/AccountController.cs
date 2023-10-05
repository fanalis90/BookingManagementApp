using API.DTOs.Accounts;
using API.Utilities.Handlers;
using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.Utilities.Handlers.Exceptions;
using API.DTOs.AccountRoles;

namespace API.Controllers
{
    //membuat endpoint routing untuk account controller 
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        //membuat account repository untuk mengakses database sebagai readonly dan private
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        //dependency injection dilakukan
        public AccountController(IAccountRepository accountRepository, IEmployeeRepository employeeRepository)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var employee = _employeeRepository.GetByEmail(forgotPasswordDto.Email);
            if(employee is null)
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
            var account = _accountRepository.GetByGuid(employee.Guid);
            if(account == null)
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
            account.OTP = GenerateOTP.GenerateOTPHandler();
            account.ExpiredTime = DateTime.Now.AddMinutes(5);
            account.IsUsed = false;
            _accountRepository.Update(account);
            return Ok(new ResponseOkHandler<ForgotPasswordResponseDto>((ForgotPasswordResponseDto)account));
        }

        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var employee = _employeeRepository.GetByEmail(changePasswordDto.Email);
            if (employee == null)
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
            var account = _accountRepository.GetByGuid(employee.Guid);
            if (account == null)
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
            if (account.OTP != changePasswordDto.Otp)
            {
                return BadRequest(new ResponseBadRequestHandler("OTP dont Match"));
            }
            if (DateTime.Now > account.ExpiredTime)
            {
                return BadRequest(new ResponseBadRequestHandler("OTP is expired"));
            }
            if (account.IsUsed == true)
            {
                return BadRequest(new ResponseBadRequestHandler("OTP is Used"));
            }
            if (changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword) 
            { 
                return BadRequest(new ResponseBadRequestHandler("Password dont Match"));
            }
            account.Password = HashHandler.HashPassword(changePasswordDto.NewPassword);
            account.IsUsed = true;
            _accountRepository.Update(account);
            return Ok(new ResponseOkHandler<AccountDto>((AccountDto) account));
        }

        //method get dari http untuk getall universities
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _accountRepository.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
            var data = result.Select(i => (AccountDto) i);
            return Ok(new ResponseOkHandler<IEnumerable<AccountDto>>(data));
        }
        //method get dari http untuk getByGuid account
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _accountRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            }
            return Ok(new ResponseOkHandler<AccountDto>((AccountDto)result));
        }
        //method post dari http untuk create account
        [HttpPost]
        public IActionResult Create(CreateAccountDto createAccountDto)
        {
            try
            {
                Account toCreate = createAccountDto;
                toCreate.Password = HashHandler.HashPassword(createAccountDto.Password);
            
                var result = _accountRepository.Create(toCreate);
                return Ok(new ResponseOkHandler<AccountDto>((AccountDto)result));

            }
            
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }
        }

        //method put dari http untuk Update account
        [HttpPut]
        public IActionResult Update(AccountDto accountDto)
        {
            try
            {
                var entity = _accountRepository.GetByGuid(accountDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Data Not Found"));

                }
                Account toUpdate = accountDto;
                toUpdate.CreatedDate = entity.CreatedDate;
                var result = _accountRepository.Update(toUpdate);
                return Ok(new ResponseOkHandler<String>("Data Updated"));

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }

        }
        //method delete dari http untuk delete account
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var account = _accountRepository.GetByGuid(guid);
                if (account is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Data Not Found"));

                }
                var result = _accountRepository.Delete(account);

                return Ok(new ResponseOkHandler<String>("Data Deleted"));
            } catch ( Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }
        }
    }
}
