using Microsoft.AspNetCore.Identity;

namespace PhysicalTherapyAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsDeleted { get; set; } 
        public string? Fname { get; set; }
        public string? Lname { get; set; }
    }
}
