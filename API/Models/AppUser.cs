using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class AppUser: IdentityUser
    {
        // Add custom fields
        public string Nickname { get; set; }
    }
}
