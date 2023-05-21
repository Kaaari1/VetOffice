using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VetOffice.Models
{
    public class Animals
    {
        [Key]
        public int id_animal { get; set; }
        [Required]
        [StringLength(50)]
        public string name_a { get; set; }
        [Required]
        [StringLength(2)]
        public string age { get; set; }
        public int id_user { get; set; }

        [ForeignKey("id_user")]
        public Users User { get; set; }
    }
}
