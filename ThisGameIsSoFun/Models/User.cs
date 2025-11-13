using Microsoft.AspNetCore.Identity;

namespace ThisGameIsSoFun.Models
{
    public class User : IdentityUser
    {
        public string Inittials { get; set; }
    }
}
