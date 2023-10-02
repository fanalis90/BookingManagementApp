using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    //membuat anotasi tabel dengan nama custom
    [Table("tb_m_accounts")]
    public class Account : BaseEntity
    {
        //tidak boleh null, menamai kolom dan tipe data spesifik
        [Required, Column("password", TypeName = "nvarchar(max)")]
        public string Password { get; set; }
        //tidak boleh null dan menamai kolom 
        [Required, Column("otp")]
        public int OTP { get; set; }
        //tidak boleh null dan menamai kolom 
        [Required, Column("is_used")]
        public bool IsUsed { get; set; }
        //tidak boleh null dan menamai kolom 
        [Required, Column("expired_time")]
        public DateTime ExpiredTime { get; set; }
        public Employee? Employee { get; set; }
        public ICollection<AccountRole>? AccountRoles { get; set; }

    }
}
