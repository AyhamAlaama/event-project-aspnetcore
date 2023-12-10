using Microsoft.AspNetCore.Identity;

namespace Event.Domain.IdentityModels.ExtendedUser
{
    public class ApplicationUser:IdentityUser
    {
        
        public ICollection<Events>? Events { get; set; }
    }
}
