using System.ComponentModel.DataAnnotations;

namespace PetShop.DTOs
{
    public class LoginDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string UserNameOrEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsPersistent { get; set; }
    }
}
