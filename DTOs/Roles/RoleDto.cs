using API.DTOs.Employees;
using BookingManagementApp.Models;
using BookingManagementApp.Utilities.Enum;

namespace API.DTOs.Roles
{
    public class RoleDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }


        //membuat implicit operator untuk update
        public static implicit operator Role(RoleDto roleDto)
        {
            return new Role
            {
             Guid = roleDto.Guid,
             Name = roleDto.Name,
             ModifiedDate = DateTime.Now

            };
        }
        //membuat explicit operator untuk response get, create , getbyid
        public static explicit operator RoleDto(Role role)
        {
            return new RoleDto
            {
               Guid = role.Guid,
               Name = role.Name,

            };
        }
    }
}
