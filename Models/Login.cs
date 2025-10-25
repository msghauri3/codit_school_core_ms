using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace codit_school_core_ms.Models
{
    [Table("Login")]
    public class Login
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

   
        [StringLength(100)]
        public string UserRole { get; set; }

        public DateTime? CreatedDate { get; set; } = DateTime.Now;
    }
}
