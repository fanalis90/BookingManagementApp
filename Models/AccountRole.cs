using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    //membuat anotasi tabel dengan nama custom
    [Table("tb_m_account_roles")]
    public class AccountRole : BaseEntity
    {
        //tidak boleh null, menamai kolom dan tipe data spesifik
        [Required, Column("account_guid")]
        public Guid AccountGuid { get; set; }
        //tidak boleh null, menamai kolom dan tipe data spesifik
        [Required, Column("role_guid")]
        public Guid RoleGuid { get; set; }

        public Account? Account { get; set; }
        public Role? Role { get; set; }

    }
}
