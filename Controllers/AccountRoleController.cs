using API.DTOs.AccountRoles;
using API.DTOs.Universities;
using API.Contracts;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using API.DTOs.Bookings;
using API.Utilities.Handlers;
using API.DTOs.Accounts;
using API.Utilities.Handlers.Exceptions;

namespace API.Controllers
{
    //membuat endpoint routing untuk accountRole controller 
    [ApiController]
    [Route("api/[controller]")]
    public class AccountRoleController : ControllerBase
    {
        //membuat accountRole repository untuk mengakses database sebagai readonly dan private
        private readonly IAccountRoleRepository _accountRoleRepository;
        //dependency injection dilakukan
        public AccountRoleController(IAccountRoleRepository accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }
        //method get dari http untuk getall universities
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _accountRoleRepository.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
            var data = result.Select(i => (AccountRoleDto) i);
            return Ok(new ResponseOkHandler<IEnumerable<AccountRoleDto>>(data));
        }
        //method get dari http untuk getByGuid accountRole
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _accountRoleRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            }
            return Ok(new ResponseOkHandler<AccountRoleDto>((AccountRoleDto) result));
        }
        //method post dari http untuk create accountRole
        [HttpPost]
        public IActionResult Create(CreateAccountRoleDto createAccountRoleDto)
        {
            try
            {

            AccountRole toCreate = createAccountRoleDto;
            var result = _accountRoleRepository.Create(toCreate);
         
            return Ok(new ResponseOkHandler<AccountRoleDto>((AccountRoleDto) result));
            } catch (Exception e) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }
        }

        //method put dari http untuk Update accountRole
        [HttpPut]
        public IActionResult Update(AccountRoleDto accountRoleDto)
        {
            try
            {
                var entity = _accountRoleRepository.GetByGuid(accountRoleDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Data Not Found"));

                }
                AccountRole toUpdate = accountRoleDto;
                toUpdate.CreatedDate = entity.CreatedDate;

                var result = _accountRoleRepository.Update(accountRoleDto);
                return Ok(new ResponseOkHandler<String>("Data Updated"));

            } catch (Exception e){
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }
        }
        //method delete dari http untuk delete accountRole
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var accountRole = _accountRoleRepository.GetByGuid(guid);
                if (accountRole is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Data Not Found"));

                }
                var result = _accountRoleRepository.Delete(accountRole);
                return Ok(new ResponseOkHandler<String>("Data Deleted"));

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Create Data", e.Message));
            }

        }
    }
}
