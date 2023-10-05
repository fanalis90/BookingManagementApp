using API.DTOs.Accounts;
using API.Models;

public class EmployeeDetailsDto
    {
        public Guid Guid { get; set; }
        public string Nik { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate {  get; set; }
        public string Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public float Gpa { get; set; }
        public string University { get; set; }

        //membuat explicit operator untuk response get, create , getbyid
        public static explicit operator EmployeeDetailsDto(RegisterDto RegisterDto)
        {
            return new EmployeeDetailsDto
            {
  
                FullName = RegisterDto.FirstName +" "+ RegisterDto.LastName,
                BirthDate = RegisterDto.BirthDate,
                Gender = RegisterDto.Gender.ToString(),
                HiringDate = RegisterDto.HiringDate,
                Email = RegisterDto.Email,
                PhoneNumber = RegisterDto.PhoneNumber,
                Major = RegisterDto.Major,
                Degree = RegisterDto.Degree,
                Gpa = RegisterDto.Gpa

            };
        }
    }

