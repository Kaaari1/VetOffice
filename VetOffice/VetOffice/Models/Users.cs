using System.ComponentModel.DataAnnotations;

namespace VetOffice.Models
{
    public class Users
    {
        [Key]
        public int id_user { get; set; }
        [Required]
        [StringLength(50)]
        public string name { get; set; }
        [Required]
        [StringLength(50)]
        public string surname { get; set; }
        [Required]
        [StringLength(9)]
        public string phone_number { get; set; }
    }
}
