using System.ComponentModel.DataAnnotations;

namespace PhysicalTherapyAPI.DTOs
{
    public class RegisterDTO
    {
        [MinLength(3)]
        [Required]
        public string Name { get; set; }
        [MinLength(6)]
        [Required]
        public string Password { get; set; }
    }
}
