﻿using BookingManagementApp.Utilities.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    [Table("tb_m_employees")]
    public class Employee : BaseEntity
    {

        [Required, Column("nik", TypeName = "nchar(6)")]
        public string NIK { get; set; }
        [Required, Column("first_name", TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }
        [Column("last_name", TypeName = "nvarchar(100)")]
        public string? LastName { get; set; }
        [Required, Column("birth_date")]
        public DateTime BirthDate { get; set; }
        [Required, Column("gender")]
        public Gender Gender { get; set; }
        [Required, Column("hiring_date")]
        public DateTime HiringDate { get; set; }
        [Required, Column("email", TypeName = "nvarchar(100)")]
        public string Email { get; set; }
        [Required, Column("phone_number", TypeName = "nvarchar(20)")]
        public string phoneNumber { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
