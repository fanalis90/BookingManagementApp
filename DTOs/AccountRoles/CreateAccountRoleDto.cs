using BookingManagementApp.Models;

namespace API.DTOs.AccountRoles
{
    public class CreateAccountRoleDto
    {
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }

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
