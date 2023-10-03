using BookingManagementApp.Models;
using BookingManagementApp.Utilities.Enum;

namespace API.DTOs.Employees
{
    public class CreateEmployeeDto
    {
        public string Nik { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


        //membuat implicit operator untuk create
        public static implicit operator Employee(CreateEmployeeDto employeeDto)
        {
            return new Employee
            {
                NIK = employeeDto.Nik,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                BirthDate = employeeDto.BirthDate,
                Gender = employeeDto.Gender,
                HiringDate = employeeDto.HiringDate,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                ModifiedDate = DateTime.Now

            };
        }
    }
}
