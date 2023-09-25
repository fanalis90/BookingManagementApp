using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Controllers
{
    public class AccountRole : Date
    {
        [Key]
        public Guid Guid { get; set; }
        [ForeignKey("Account")]
        public Guid AccounGuid { get; set; }
        [ForeignKey("Role")]
        public Guid RoleGuid { get; set; }

    }
}
