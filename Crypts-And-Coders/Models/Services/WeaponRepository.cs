using Crypts_And_Coders.Data;
using Crypts_And_Coders.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crypts_And_Coders.Models.Services
{
    public class WeaponRepository : IWeapon
    {
        private CryptsDbContext _context;

        public WeaponRepository(CryptsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new weapon in the database
        /// </summary>
        /// <param name="weapon">Weapon information for creation</param>
        /// <returns>Successful result of weapon creation</returns>
        public async Task<Weapon> Create(Weapon weapon)
        {
            _context.Entry(weapon).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return weapon;
        }

        /// <summary>
        /// Delete a weapon from the database
        /// </summary>
        /// <param name="id">Id of weapon to be deleted</param>
        /// <returns>Task of completion for weapon delete</returns>
        public async Task Delete(int id)
        {
            Weapon weapon = await _context.Weapon.FindAsync(id);
            if (weapon != null)
            {
                _context.Entry(weapon).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get a specific weapon in the database by ID
        /// </summary>
        /// <param name="id">Id of weapon to search for</param>
        /// <returns>Successful result of specified weapon</returns>
        public async Task<Weapon> GetWeapon(int id)
        {
            var result = await _context.Weapon.Where(x => x.Id == id)
                                                 .FirstOrDefaultAsync();

            return result;
        }

        /// <summary>
        /// Get a list of all weapons in the database
        /// </summary>
        /// <returns>Successful result with list of weapons</returns>
        public async Task<List<Weapon>> GetWeapons()
        {
            List<Weapon> result = await _context.Weapon.ToListAsync();
            return result;
        }

        /// <summary>
        /// Update a given weapon in the database
        /// </summary>
        /// <param name="id">Id of location to be updated</param>
        /// <param name="weapon">Weapon information for update</param>
        /// <returns>Successful result of specified updated weapon</returns>
        public async Task<Weapon> Update(Weapon weapon)
        {
            _context.Entry(weapon).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return weapon;
        }
    }
}