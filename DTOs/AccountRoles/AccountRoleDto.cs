using API.Models;

namespace API.DTOs.AccountRoles
{
    public class AccountRoleDto
    {
        public Guid Guid { get; set; }
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }


        //membuat explicit operator untuk response get, create , getbyid
        public static explicit operator AccountRoleDto(AccountRole accountRole) {
            return new AccountRoleDto { 
                AccountGuid = accountRole.AccountGuid, 
                RoleGuid = accountRole.RoleGuid };
        }

        //membuat implicit operator untuk update
        public static implicit operator AccountRole(AccountRoleDto accountRoleDto)
        {
            return new AccountRole
            {
                Guid = accountRoleDto.Guid,
                AccountGuid = accountRoleDto.AccountGuid,
                RoleGuid = accountRoleDto.RoleGuid,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
