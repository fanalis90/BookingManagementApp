﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    //membuat anotasi tabel dengan nama custom
    [Table("tb_m_rooms")]
    public class Room : BaseEntity
    {
        //tidak boleh null, menamai kolom dan tipe data spesifik
        [Required, Column("name", TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        //tidak boleh null dan menamai kolom 
        [Required, Column("floor")]
        public int Floor { get; set; }
        //tidak boleh null dan menamai kolom 
        [Required, Column("capacity")]
        public int Capacity { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}