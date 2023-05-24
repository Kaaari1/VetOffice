using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VetOffice.Models
{
    public class Presence
    {
        [Key]
        public int dayoff_id { get; set; }
        [ForeignKey("Doctor")]
        public int id_doctor { get; set; }
        public bool is_active { get; set; } = true;
        public DateTime nworking_days { get; set; }

        public Doctors Doctor { get; set; }
    }
}
