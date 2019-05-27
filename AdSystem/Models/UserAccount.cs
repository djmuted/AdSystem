using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AdSystem.Models
{
    public class UserAccount
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(32)]
        public string username { get; set; }
        [Required]
        [MaxLength(1024)]
        public string password { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid apiKey { get; set; }
    }
}
