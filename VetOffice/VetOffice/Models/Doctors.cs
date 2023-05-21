using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VetOffice.Models
{
    public class Doctors
    {
        [Key]
        public int id_doctor { get; set; }
        [Required]
        [StringLength(100)]
        public string nameands { get; set; }
    }
}
