using Crypts_And_Coders.Data;
using Crypts_And_Coders.Models.DTOs;
using Crypts_And_Coders.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Crypts_And_Coders.Models.SpeciesAndClass;

namespace Crypts_And_Coders.Models.Services
{
    public class CharacterRepository : ICharacter
    {
        private readonly CryptsDbContext _context;
        private readonly ICharacterStat _characterStat;
        private readonly IWeapon _weapon;
        private readonly ILocation _location;

        public CharacterRepository(CryptsDbContext context, ICharacterStat characterStat, IWeapon weapon, ILocation location)
        {
            _context = context;
            _characterStat = characterStat;
            _weapon = weapon;
            _location = location;
        }

        /// <summary>
        /// Creates a new character in the database
        /// </summary>
        /// <param name="character">Character information for creation</param>
        /// <returns>Successful result of character creation</returns>
        public async Task<CharacterDTO> Create(CharacterDTO characterDTO)
        {
            Enum.TryParse(characterDTO.Species, out Species species);
            Enum.TryParse(characterDTO.Class, out Class userClass);

            Character character = new Character()
            {
                Name = characterDTO.Name,
                Class = userClass,
                Species = species,
                WeaponId = characterDTO.WeaponId,
                LocationId = characterDTO.LocationId,
                UserName = characterDTO.UserName
            };
            _context.Entry(character).State = EntityState.Added;
            await _context.SaveChangesAsync();
            characterDTO.Id = character.Id;
            characterDTO.Weapon = await _weapon.GetWeapon(characterDTO.WeaponId);
            characterDTO.CurrentLocation = await _location.GetLocation(characterDTO.LocationId);
            characterDTO.Inventory = new List<InventoryDTO>();
            characterDTO.StatSheet = new List<CharacterStatDTO>();
            return characterDTO;
        }

        /// <summary>
        /// Delete a character from the database
        /// </summary>
        /// <param name="id">Id of character to be deleted</param>
        /// <returns>Task of completion for character delete</returns>
        public async Task<bool> Delete(int id)
        {
            Character character = await _context.Character.FindAsync(id);
            if (character != null)
            {
                _context.Entry(character).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get a specific character in the database by ID
        /// </summary>
        /// <param name="id">Id of character to search for</param>
        /// <returns>Successful result of specified character</returns>
        public async Task<CharacterDTO> GetCharacter(int id)
        {
            var result = await _context.Character.Where(x => x.Id == id).Include(x => x.Inventory).ThenInclude(x => x.Item).FirstOrDefaultAsync();
            CharacterDTO resultDTO = new CharacterDTO()
            {
                Id = result.Id,
                Name = result.Name,
                Species = result.Species.ToString(),
                Class = result.Class.ToString(),
                WeaponId = result.WeaponId,
                Weapon = await _weapon.GetWeapon(result.WeaponId),
                LocationId = result.LocationId,
                CurrentLocation = await _location.GetLocation(result.LocationId),
                UserName = result.UserName
            };
            //result.DTO.Weapon = _weapons.GetWeapon(result.weaponId)
            var stats = await _characterStat.GetCharacterStats(id);
            resultDTO.StatSheet = stats;
            var items = result.Inventory;
            resultDTO.Inventory = await GetPlayerItems(resultDTO.Id);
            return resultDTO;
        }

        /// <summary>
        /// Get a specific character in the database by ID synchronously (Needed for user validation)
        /// </summary>
        /// <param name="id">Id of character to search for</param>
        /// <returns>String of character's name or string "null" if not found</returns>
        public string GetCharacterUserNameSync(int id)
        {
            string result = _context.Character.Where(x => x.Id == id).Select(x => x.UserName != null ? x.UserName : "null").FirstOrDefault().ToString();
            return result;
        }

        /// <summary>
        /// Get a list of all characters in the database
        /// </summary>
        /// <returns>Successful result with list of characters</returns>
        public async Task<List<CharacterDTO>> GetCharacters()
        {
            List<Character> result = await _context.Character.ToListAsync();
            List<CharacterDTO> resultDTO = new List<CharacterDTO>();
            foreach (var item in result)
            {
                resultDTO.Add(await GetCharacter(item.Id));
            }
            return resultDTO;
        }

        /// <summary>
        /// Update a given character in the database
        /// </summary>
        /// <param name="id">Id of character to be updated</param>
        /// <param name="character">Character information for update</param>
        /// <returns>Successful result of specified updated character</returns>
        public async Task<CharacterDTO> Update(CharacterDTO characterDTO)
        {
            Enum.TryParse(characterDTO.Species, out Species species);
            Enum.TryParse(characterDTO.Class, out Class userClass);

            Character character = new Character()
            {
                Id = characterDTO.Id,
                Name = characterDTO.Name,
                Class = userClass,
                Species = species,
                WeaponId = characterDTO.WeaponId,
                LocationId = characterDTO.LocationId,
                UserName = characterDTO.UserName
            };

            _context.Entry(character).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            characterDTO.Weapon = await _weapon.GetWeapon(characterDTO.WeaponId);
            characterDTO.CurrentLocation = await _location.GetLocation(characterDTO.LocationId);
            characterDTO.Inventory = await GetPlayerItems(characterDTO.Id);
            characterDTO.StatSheet = await _characterStat.GetCharacterStats(characterDTO.Id);
            return characterDTO;
        }

        /// <summary>
        /// Add an item to a character's inventory
        /// </summary>
        /// <param name="charId">Id of character</param>
        /// <param name="itemId">Id of item</param>
        /// <returns>Successful result of item addition</returns>
        public async Task AddItemToInventory(int charId, int itemId)
        {
            CharacterInventory inventory = new CharacterInventory()
            {
                CharacterId = charId,
                ItemId = itemId
            };

            _context.Entry(inventory).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Remove an item from a character's inventory
        /// </summary>
        /// <param name="charId">Id of character</param>
        /// <param name="itemId">Id of item</param>
        /// <returns>Successful result of item removal</returns>
        public async Task RemoveItemFromInventory(int charId, int itemId)
        {
            CharacterInventory result = await _context.CharacterInventory.FindAsync(charId, itemId);

            if (result != null)
            {
                _context.Entry(result).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get a list of a character's items
        /// </summary>
        /// <param name="charId">Unique character ID</param>
        /// <returns>Successful result of list of items in inventory</returns>
        public async Task<List<InventoryDTO>> GetPlayerItems(int charId)
        {
            var result = await _context.CharacterInventory.Where(x => x.CharacterId == charId).Include(x => x.Item).ToListAsync();
            List<InventoryDTO> resultDTO = new List<InventoryDTO>();
            foreach (var item in result)
            {
                resultDTO.Add(new InventoryDTO()
                {
                    CharacterId = item.CharacterId,
                    Item = new ItemDTO()
                    {
                        Name = item.Item.Name,
                        Value = item.Item.Value,
                        Id = item.Item.Id
                    },
                    ItemId = item.ItemId
                });
            }
            return resultDTO;
        }
    }
}