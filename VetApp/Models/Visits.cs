using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetOffice.Models
{
    public class Visits
    {
        [Key]
        public int id_visit { get; set; }
        public int id_animal { get; set; }
        public int id_doctor { get; set; }
        public int id_user { get; set; }
        public bool is_active { get; set; } = true;
        public DateTime date { get; set; }

        [ForeignKey("id_animal")]
        public Animals Animal { get; set; }

        [ForeignKey("id_doctor")]
        public Doctors Doctor { get; set; }

        [ForeignKey("id_user")]
        public Users User { get; set; }
    }
}
