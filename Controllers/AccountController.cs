using API.DTOs.Accounts;
using API.Utilities.Handlers;
using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.Utilities.Handlers.Exceptions;
using API.DTOs.AccountRoles;
using API.Repositories;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    //membuat endpoint routing untuk account controller 
    [ApiController]
    [Route("api/[controller]")]
    [Authorize()]
    public class AccountController : ControllerBase
    {
        //membuat account repository untuk mengakses database sebagai readonly dan private
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IEmailHandler _emailHandler;
        private readonly IJWTokenHandler _tokenHandler;
        //dependency injection dilakukan
        public AccountController(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, IEducationRepository educationRepository, IUniversityRepository universityRepository, IEmailHandler emailHandler, IJWTokenHandler tokenHandler, IAccountRoleRepository accountRoleRepository, IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _educationRepository = educationRepository;
            _universityRepository = universityRepository;
            _emailHandler = emailHandler;
            _tokenHandler = tokenHandler;
            _accountRoleRepository = accountRoleRepository;
            _roleRepository = roleRepository;
        }

        [HttpPost("ForgotPassword")]
        [AllowAnonymous]
        public IActionResult ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            try
            {
                var employee = _employeeRepository.GetByEmail(forgotPasswordDto.Email);
                if (employee is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Data Not Found"));
                }
                var account = _accountRepository.GetByGuid(employee.Guid);
                if (account == null)
                {
                    return NotFound(new ResponseNotFoundHandler("Account Not Found"));
                }
                //generate otp
                account.OTP = new Random().Next(100000, 1000000);
                //add 5 menit untuk otp
                account.ExpiredTime = DateTime.Now.AddMinutes(5);
                account.IsUsed = false;
                _accountRepository.Update(account);
                //penggunaan email service 
                _emailHandler.Send("Forgot Password", $"Your OTP is {account.OTP}", forgotPasswordDto.Email);
                return Ok(new ResponseOkHandler<ForgotPasswordResponseDto>((ForgotPasswordResponseDto)account));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Login Failed", ex.Message));
            }
           
        }

        [HttpPost("ChangePassword")]
        [AllowAnonymous]
        public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var employee = _employeeRepository.GetByEmail(changePasswordDto.Email);
            if (employee == null)
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
            //mengambil objek axxount by id employee
            var account = _accountRepository.GetByGuid(employee.Guid);
            if (account == null)
            {
                return NotFound(new ResponseNotFoundHandler("Account Not Found"));
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
                return BadRequest(new ResponseBadRequestHandler("OTP is already Used"));
            }
            if (changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword) 
            { 
                return BadRequest(new ResponseBadRequestHandler("Password dont Match"));
            }
            //hash password baru
            account.Password = HashHandler.HashPassword(changePasswordDto.NewPassword);
            account.IsUsed = true;
            _accountRepository.Update(account);
            return Ok(new ResponseOkHandler<AccountDto>((AccountDto) account));
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login(LoginDto loginDto)
        {
            //mengambil objek employe by email
            var employee = _employeeRepository.GetByEmail(loginDto.Email);
            if (employee is null)
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
            var claims = new List<Claim>();
            claims.Add(new Claim("Email", employee.Email));
            claims.Add(new Claim("Fullname", string.Concat(employee.FirstName + " " + employee.LastName)));

            var getRoleName = from ar in _accountRoleRepository.GetAll()
                              join r in _roleRepository.GetAll() on ar.RoleGuid equals r.Guid
                              where ar.AccountGuid == account.Guid
                              select r.Name;

            foreach (var roleName in getRoleName) {
                claims.Add(new Claim(ClaimTypes.Role, roleName));
            
            }
            var token = _tokenHandler.Generate(claims);
            return Ok(new ResponseOkHandler<Object>("Login Success", new {Token = token}));
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public IActionResult Register(RegisterDto registerDto)
        {
            //mengambil objek employee
            using var context = _accountRepository.GetContext();
            using var transaction = context.Database.BeginTransaction();

            try
            {

                var employee = _employeeRepository.GetByEmail(registerDto.Email);
                if (employee is null)
                {
                    //meng insert apabila tidak ada
                    employee = registerDto;
                    employee.NIK = GenerateNIKHandler.GenerateNIK(_employeeRepository.GetLastNik());
                    employee = _employeeRepository.Create(employee);
                }
                else
                {
                    //return badrequest apabila ada s
                    return BadRequest(new ResponseBadRequestHandler("Email is Used"));
                }
                //mengambil objek university by code
                var university = _universityRepository.GetUniversityNameByCode(registerDto.UniversityCode);
                if (university is null)
                {
                    //apabila tidak ada, insert
                    university = _universityRepository.Create(registerDto);
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

                _accountRepository.Create(account);

                _accountRoleRepository.Create(new AccountRole
                {
                    Guid = new Guid(),
                    AccountGuid = account.Guid,
                    RoleGuid = _roleRepository.GetRoleGuid() ?? throw new Exception("default row not found")
                }) ;

                transaction.Commit();
                return Ok(new ResponseOkHandler<string>("Account Created"));
            } catch (Exception ex)
            {
                transaction.Rollback();
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to create account", ex.Message));
            }
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
