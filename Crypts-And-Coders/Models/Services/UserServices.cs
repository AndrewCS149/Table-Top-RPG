using Crypts_And_Coders.Models.Interfaces;
using System.Security.Claims;

namespace Crypts_And_Coders.Models.Services
{
    public class UserServices
    {
        /// <summary>
        /// User validation to make sure a player can only view and edit their own character's info
        /// </summary>
        /// <param name="User">Claim's principal of the current user</param>
        /// <param name="_character">Character Repo injection</param>
        /// <param name="id">Id of character in question</param>
        /// <returns>Bool representing if user passed validation</returns>
        public static bool ValidateUser(ClaimsPrincipal User, ICharacter _character, int id)
        {
            string userNameInDB = _character.GetCharacterUserNameSync(id);
            //Only allow players to view their own character's information
            string userName = User.FindFirst("UserName").Value;

            if (User.IsInRole("Player") && userName != userNameInDB)
            {
                return false;
            }
            return true;
        }
    }
}