using Microsoft.AspNetCore.Identity;

namespace Simulation_16.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
    }
}
