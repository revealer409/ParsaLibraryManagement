using Microsoft.AspNetCore.Identity;
 

namespace ParsaLibraryManagement.Domain.Entities
{
    public class Role:IdentityRole<int>
    {
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
