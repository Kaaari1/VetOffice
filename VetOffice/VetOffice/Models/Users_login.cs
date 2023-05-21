using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace VetOffice.Models
{
    public class Users_login
    {
        [Key]
        public int id_user { get; set; }
        [Required]
        [StringLength(100)]
        public string email { get; set; }
        [Required]
        [StringLength(32)]
        public string password { get; set; }
        public int id_role { get; set; }
        [Required]
        [StringLength(50)]
        public string role { get; set; }

        [ForeignKey("id_user")]
        public Users User { get; set; }

        [ForeignKey("id_role")]
        public Roles Role { get; set; }
    }
}
