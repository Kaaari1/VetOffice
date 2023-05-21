using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VetOffice.Models
{
    public class Presence
    {
        [Key]
        [ForeignKey("Doctor")]
        public int id_doctor { get; set; }
        public DateTime work_hours { get; set; }
        public DateTime nworking_days { get; set; }

        public Doctors Doctor { get; set; }
    }
}
