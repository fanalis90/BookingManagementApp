﻿using BookingManagementApp.Utilities.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    //membuat anotasi tabel dengan nama custom
    [Table("tb_tr_bookings")]
    public class Booking : BaseEntity
    {
        //tidak boleh null dan menamai kolom 
        [Required, Column("start_date")]
        public DateTime StartDate { get; set; }
        //tidak boleh null dan menamai kolom 
        [Required, Column("status")]
        public StatusLevel Status { get; set; }
        //tidak boleh null, menamai kolom dan tipe data spesifik
        [Required, Column("remarks", TypeName = "nvarchar")]
        public string Remarks { get; set; } 
        //tidak boleh null dan menamai kolom 
        [Required, Column("employee_guid")]
        public Guid EmployeeGuid { get; set; }
        //tidak boleh null dan menamai kolom 
        [Required, Column("room_guid")]
        public Guid RoomGuid { get; set; }
        public Employee? Employee { get; set; }
        public Room? Room { get; set; }
    }
}