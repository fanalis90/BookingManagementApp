﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    //membuat anotasi tabel dengan nama custom
    [Table("tb_m_educations")]
    public class Education : BaseEntity
    {
        //tidak boleh null, menamai kolom dan tipe data spesifik 
        [Required, Column("major", TypeName = "nvarchar(100)")]
        public string Major { get; set; }
        //tidak boleh null, menamai kolom dan tipe data spesifik
        [Required, Column("degree", TypeName = "nvarchar(100)")]
        public string Degree { get; set; }
        //tidak boleh null dan menamai kolom 
        [Required, Column("gpa")]
        public float GPA { get; set; }
        //tidak boleh null, menamai kolom dan tipe data spesifik
        [Required, Column("university_guid")]
        public Guid UniversityGuid { get; set; }

        public University? University { get; set; }
        public Employee? Employee {  get; set; }

    }
}
