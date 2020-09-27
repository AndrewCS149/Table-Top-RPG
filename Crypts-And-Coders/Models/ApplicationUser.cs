using Microsoft.AspNetCore.Identity;

namespace Crypts_And_Coders.Models
{
    public class ApplicationUser : IdentityUser
    {
    }

    public static class ApplicationRoles
    {
        public const string GameMaster = "Game Master";
        public const string Player = "Player";
    }
}