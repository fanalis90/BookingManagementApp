using BookingManagementApp.Models;

namespace API.DTOs.AccountRoles
{
    public class CreateAccountRoleDto
    {
        public Guid Guid { get; set; }
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }


        //membuat implicit operator untuk create
        public static implicit operator AccountRole(CreateAccountRoleDto createAccountRoleDto)
        {
            return new AccountRole
            {
                RoleGuid = createAccountRoleDto.RoleGuid,
                AccountGuid = createAccountRoleDto.AccountGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
