using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop_Core.Models
{
    public class Professional: BaseEntity
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Fullname { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(250)]
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile? ImgFile { get; set; }
    }
}
