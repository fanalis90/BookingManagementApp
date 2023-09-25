using BookingManagementApp.Utilities.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    [Table("tb_tr_bookings")]
    public class Booking : BaseEntity
    {
        [Required, Column("start_date")]
        public DateTime StartDate { get; set; }
        [Required, Column("status")]
        public StatusLevel Status { get; set; }
        [Required, Column("remarks", TypeName = "nvarchar")]
        public string Remarks { get; set; }
        [ForeignKey("Room"), Column("room_guid")]
        public Guid RoomGuid { get; set; }
        [ForeignKey("Employee"), Column("employee_id")]
        public Guid EmployeeGuid { get; set; }

    }
}
