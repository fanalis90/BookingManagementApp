using BookingManagementApp.Utilities.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    //membuat anotasi tabel dengan nama custom
    [Table("tb_m_employees")]
    public class Employee : BaseEntity
    {
        //tidak boleh null, menamai kolom dan tipe data spesifik
        [Required, Column("nik", TypeName = "nchar(6)")]
        public string NIK { get; set; }
        //tidak boleh null, menamai kolom dan tipe data spesifik
        [Required, Column("first_name", TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }
        //tidak boleh null, menamai kolom dan tipe data spesifik
        [Column("last_name", TypeName = "nvarchar(100)")]
        public string? LastName { get; set; }
        //tidak boleh null dan menamai kolom 
        [Required, Column("birth_date")]
        public DateTime BirthDate { get; set; }
        //tidak boleh null dan menamai kolom 
        [Required, Column("gender")]
        public Gender Gender { get; set; }
        //tidak boleh null dan menamai kolom 
        [Required, Column("hiring_date")]
        public DateTime HiringDate { get; set; }
        //tidak boleh null, menamai kolom dan tipe data spesifik
        [Required, Column("email", TypeName = "nvarchar(100)")]
        public string Email { get; set; }
        //tidak boleh null, menamai kolom dan tipe data spesifik
        [Required, Column("phone_number", TypeName = "nvarchar(20)")]
        public string PhoneNumber { get; set; }
        public Education? Education {  get; set; }
        public Account? Account {  get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}
