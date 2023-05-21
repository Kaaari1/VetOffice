using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VetOffice.Models
{
    public class Visits
    {
        [Key]
        public int id_visit { get; set; }
        public int id_animal { get; set; }
        [Required]
        [StringLength(50)]
        public string name_a { get; set; }
        public int id_doctor { get; set; }
        [Required]
        [StringLength(100)]
        public string nameands { get; set; }
        public int id_user { get; set; }
        [Required]
        [StringLength(50)]
        public string name { get; set; }
        [Required]
        [StringLength(50)]
        public string surname { get; set; }
        public DateTime date { get; set; }

        [ForeignKey("id_animal")]
        public Animals Animal { get; set; }

        [ForeignKey("id_doctor")]
        public Doctors Doctor { get; set; }

        [ForeignKey("id_user")]
        public Users User { get; set; }
    }
}
