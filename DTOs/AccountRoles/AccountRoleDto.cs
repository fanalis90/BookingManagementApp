using BookingManagementApp.Models;

namespace API.DTOs.AccountRoles
{
    public class AccountRoleDto
    {
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }

        public static explicit operator AccountRoleDto(AccountRole accountRole) {
            return new AccountRoleDto { 
                AccountGuid = accountRole.AccountGuid, 
                RoleGuid = accountRole.RoleGuid };
        }
        
        public static implicit operator AccountRole(AccountRoleDto accountRoleDto)
        {
            return new AccountRole
            {
                AccountGuid = accountRoleDto.AccountGuid,
                RoleGuid = accountRoleDto.RoleGuid,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
