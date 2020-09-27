using System.ComponentModel.DataAnnotations;

namespace Crypts_And_Coders.Models.DTOs
{
    public class RegisterDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Role { get; set; }
    }
}