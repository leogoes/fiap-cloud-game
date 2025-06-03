using Microsoft.AspNetCore.Identity;

namespace FIAP.Cloud.Games.Identity.Data.Models
{
    public class CurrentUser : IdentityUser
    {
        public CurrentUser(string email, string name)
        {
            Email = email;
            UserName = email;
            NormalizedUserName = name;
        }

        public CurrentUser()
        {
            
        }
    }
}
