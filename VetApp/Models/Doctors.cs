using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VetOffice.Models
{
    public class Doctors
    {
        [Key]
        public int id_doctor { get; set; }
        public int id_user { get; set; }
        [Required]
        public TimeSpan work_hours_from { get; set; }
        [Required]
        public TimeSpan work_hours_to { get; set; }

        [ForeignKey("id_user")]
        public Users User { get; set; }
    }
}
