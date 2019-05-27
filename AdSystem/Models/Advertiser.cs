using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AdSystem.Models
{
    public class Advertiser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        [Required]
        public int accountId { get; set; }
        [Required]
        [ForeignKey("accountId")]
        public UserAccount account { get; set; }
        [Required]
        public string openRtbUrl { get; set; }
    }
}
