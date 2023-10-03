//using API.Contracts;
//using API.DTOs.Employees;
//using API.Models;
//using API.Repositories;
//using API.Utilities.Handlers;
//using Microsoft.AspNetCore.Mvc;

//namespace API.Controllers
//{
//    public class GeneralController<TRepository, TDto, TCreateDto, TEntity> : ControllerBase where TRepository : IGeneralRepository<TEntity> where TEntity : class
//    {

 
//        //membuat repository untuk mengakses database sebagai readonly dan private
//        private readonly TRepository _tRepository;
//        //dependency injection dilakukan
//        public GeneralController(TRepository tRepository)
//        {
//            _tRepository = tRepository;
//        }
//        //method get dari http untuk getall universities
//        [HttpGet]
//        public IActionResult GetAll()
//        {
//            var result = _tRepository.GetAll();
//            if (!result.Any())
//            {
//                return NotFound("Data Not Found");
//            }

//            return Ok(result);
//        }
//        //method get dari http untuk getByGuid employee
//        [HttpGet("{guid}")]
//        public IActionResult GetByGuid(Guid guid)
//        {
//            var result = _tRepository.GetByGuid(guid);
//            if (result is null)
//            {
//                return NotFound("Data Not Found");

//            }
//            return Ok(result);
//        }
//        //method post dari http untuk create employee
//        [HttpPost]
//        public IActionResult Create(TCreateDto createEmployeeDto)
//        {
//            TEntity toCreate = createEmployeeDto;
//            var result = _tRepository.Create(toCreate);
//            if (result is null)
//            {
//                return BadRequest("Failed To Create Data");
//            }
//            return Ok(result);
//        }

//        //method put dari http untuk Update employee
//        [HttpPut]
//        public IActionResult Update(EmployeeDto employeeDto)
//        {
//            var entity = _tRepository.GetByGuid(employeeDto.Guid);
//            if (entity is null)
//            {
//                return NotFound("Data Not Found");

//            }
//            Employee toUpdate = employeeDto;
//            toUpdate.CreatedDate = entity.CreatedDate;
//            var result = _tRepository.Update(employeeDto);
//            if (!result)
//            {
//                return BadRequest("Failed To Update Data");
//            }

//            return Ok(result);
//        }
//        //method delete dari http untuk delete employee
//        [HttpDelete("{guid}")]
//        public IActionResult Delete(Guid guid)
//        {
//            var employee = _tRepository.GetByGuid(guid);
//            if (employee is null)
//            {
//                return NotFound("Data Not Found");

//            }
//            var result = _tRepository.Delete(employee);
//            if (!result)
//            {
//                return BadRequest("Failed To Delete Data");
//            }

//            return Ok(result);
//        }
//    }
//}
//}
