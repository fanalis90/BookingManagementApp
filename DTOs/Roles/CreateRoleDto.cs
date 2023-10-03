using API.DTOs.Employees;
using BookingManagementApp.Models;
using BookingManagementApp.Utilities.Enum;

namespace API.DTOs.Roles
{
    public class CreateRoleDto
    {
        public string Name { get; set; }
    

        //membuat implicit operator untuk create
        public static implicit operator Role(CreateRoleDto roleDto)
        {
            return new Role
            {
                Name = roleDto.Name,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now

            };
        }
    }
}
