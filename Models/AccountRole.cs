using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    [Table("tb_m_account_roles")]
    public class AccountRole : BaseEntity
    {

        [Required,ForeignKey("Account"), Column("account_guid")]
        public Guid AccountGuid { get; set; }
        [Required,ForeignKey("Role"), Column("role_guid")]
        public Guid RoleGuid { get; set; }

    }
}
