﻿using API.DTOs.Employees;
using BookingManagementApp.Models;
using BookingManagementApp.Utilities.Enum;

namespace API.DTOs.Employees
{
    public class EmployeeDto
    {
        public Guid Guid { get; set; }
        public string Nik { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate {  get; set; }
        public Gender Gender { get; set; }
        public DateTime HiringDate {  get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


        //membuat implicit operator untuk update
        public static implicit operator Employee(EmployeeDto employeeDto)
        {
            return new Employee
            {
                Guid = employeeDto.Guid,
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
        //membuat explicit operator untuk response get, create , getbyid
        public static explicit operator EmployeeDto(Employee employee)
        {
            return new EmployeeDto
            {
                Nik = employee.NIK,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BirthDate = employee.BirthDate,
                Gender = employee.Gender,
                HiringDate = employee.HiringDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,

            };
        }

    }
}
