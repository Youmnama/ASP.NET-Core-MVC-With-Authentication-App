using Microsoft.AspNetCore.Identity;

namespace RoleBased.Models
{
    public class User: IdentityUser
    {
        public  string FullName { get; set; }
    }
}
