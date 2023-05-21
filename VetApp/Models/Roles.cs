using System.ComponentModel.DataAnnotations;

namespace VetOffice.Models
{
    public class Roles
    {
        [Key]
        public int id_role { get; set; }
        [Required]
        [StringLength(50)]
        public string role_name { get; set; }
    }
}
