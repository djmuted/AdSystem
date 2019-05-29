using Flurl;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AdSystem.Models
{
    public class Ad
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid id { get; set; }
        [Required]
        [MaxLength(32)]
        public string name { get; set; }
        [JsonIgnore]
        [Required]
        public Advertiser advertiser { get; set; }
        [NotMapped]
        public string href { get { return "/api/advertiser/ads/" + this.id.ToString("N"); } }
        [NotMapped]
        public string bannerUrl { get { return Url.Combine(new string[] { "/adimg/" + this.id + ".png" }); } }
    }
}
