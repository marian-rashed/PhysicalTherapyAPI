using Microsoft.AspNetCore.Identity;

namespace PhysicalTherapyAPI.Models
{
    public class ApplicationRole : IdentityRole
    {
        public bool IsDeleted { get; set; } 
    }
}
