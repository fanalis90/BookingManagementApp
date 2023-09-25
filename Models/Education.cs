using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    [Table("tb_m_educations")]
    public class Education : BaseEntity
    {
        [Required, Column("major", TypeName = "nvarchar(100)")]
        public string Major { get; set; }
        [Required, Column("degree", TypeName = "nvarchar(100)")]
        public string Degree { get; set; }
        [Required, Column("gpa")]
        public float GPA { get; set; }
        [ForeignKey("University"), Column("university_guid")]
        public Guid UniversityGuid { get; set; }

    }
}
