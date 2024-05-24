using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Team:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Position { get; set; }
        public string? XUrl { get; set; }
        public string? FacebookUrl { get; set; }
        public string? LinkedUrl { get; set; }

        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
