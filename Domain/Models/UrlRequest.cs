using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UrlRequest
    {
        [Required]
        [Url]
        public string? OriginalUrl { get; set; }
        public DateTime? ExpirationTime { get; set; }
    }
}
