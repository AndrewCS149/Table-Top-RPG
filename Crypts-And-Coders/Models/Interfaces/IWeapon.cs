using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crypts_And_Coders.Models.Interfaces
{
    public interface IWeapon
    {
        /// <summary>
        /// Creates a new weapon in the database
        /// </summary>
        /// <param name="weapon">Weapon information for creation</param>
        /// <returns>Successful result of weapon creation</returns>
        Task<Weapon> Create(Weapon weapon);

        /// <summary>
        /// Get a list of all weapons in the database
        /// </summary>
        /// <returns>Successful result with list of weapons</returns>
        Task<List<Weapon>> GetWeapons();

        /// <summary>
        /// Get a specific weapon in the database by ID
        /// </summary>
        /// <param name="id">Id of weapon to search for</param>
        /// <returns>Successful result of specified weapon</returns>
        Task<Weapon> GetWeapon(int id);

        /// <summary>
        /// Update a given weapon in the database
        /// </summary>
        /// <param name="weapon">Weapon information for update</param>
        /// <returns>Successful result of specified updated weapon</returns>
        Task<Weapon> Update(Weapon weapon);

        /// <summary>
        /// Delete a weapon from the database
        /// </summary>
        /// <param name="id">Id of weapon to be deleted</param>
        /// <returns>Task of completion for weapon delete</returns>
        Task Delete(int id);
    }
}