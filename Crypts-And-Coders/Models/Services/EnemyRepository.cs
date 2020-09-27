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
    public class EnemyRepository : IEnemy
    {
        private readonly CryptsDbContext _context;

        public EnemyRepository(CryptsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new enemy in the database
        /// </summary>
        /// <param name="enemyDTO">enemy information for creation</param>
        /// <returns>Successful result of enemy creation</returns>
        public async Task<EnemyDTO> Create(EnemyDTO enemyDTO)
        {
            Enum.TryParse(enemyDTO.Species, out Species species);
            Enemy enemy = new Enemy()
            {
                Abilities = enemyDTO.Abilities,
                Type = enemyDTO.Type,
                Species = species
            };

            _context.Entry(enemy).State = EntityState.Added;
            await _context.SaveChangesAsync();
            enemyDTO.Id = enemy.Id;
            enemyDTO.Loot = new List<EnemyLootDTO>();
            return enemyDTO;
        }

        /// <summary>
        /// Delete a enemy from the database
        /// </summary>
        /// <param name="id">Id of enemy to be deleted</param>
        /// <returns>Task of completion for enemy delete</returns>
        public async Task Delete(int id)
        {
            Enemy enemy = await _context.Enemy.FindAsync(id);
            if (enemy != null)
            {
                _context.Entry(enemy).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get a list of all enemies in the database
        /// </summary>
        /// <returns>Successful result with list of enemies</returns>
        public async Task<List<EnemyDTO>> GetEnemies()
        {
            List<Enemy> enemies = await _context.Enemy.ToListAsync();
            List<EnemyDTO> enemyDTO = new List<EnemyDTO>();

            foreach (var item in enemies)
            {
                enemyDTO.Add(await GetEnemy(item.Id));
            }

            return enemyDTO;
        }

        /// <summary>
        /// Get a specific enemy in the database by ID
        /// </summary>
        /// <param name="id">Id of enemy to search for</param>
        /// <returns>Successful result of specified enemy</returns>
        public async Task<EnemyDTO> GetEnemy(int id)
        {
            Enemy enemy = await _context.Enemy.Where(x => x.Id == id).FirstOrDefaultAsync();

            EnemyDTO enemyDTO = new EnemyDTO()
            {
                Id = enemy.Id,
                Abilities = enemy.Abilities,
                Type = enemy.Type,
                Species = enemy.Species.ToString()
            };
            enemyDTO.Loot = await GetEnemyLoot(enemyDTO.Id);
            return enemyDTO;
        }

        /// <summary>
        /// Update a given enemy in the database
        /// </summary>
        /// <param name="enemyDTO">Enemy information for update</param>
        /// <returns>Successful result of specified updated enemy</returns>
        public async Task<EnemyDTO> Update(EnemyDTO enemyDTO)
        {
            Enum.TryParse(enemyDTO.Species, out Species species);

            Enemy enemy = new Enemy()
            {
                Id = enemyDTO.Id,
                Abilities = enemyDTO.Abilities,
                Type = enemyDTO.Type,
                Species = species
            };

            _context.Entry(enemy).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            enemyDTO.Loot = await GetEnemyLoot(enemyDTO.Id);

            return enemyDTO;
        }

        /// <summary>
        /// Add an item to a enemy's loot
        /// </summary>
        /// <param name="enemyId">Id of enemy</param>
        /// <param name="itemId">Id of item</param>
        /// <returns>Successful result of item addition</returns>
        public async Task AddItemToLoot(int enemyId, int itemId)
        {
            EnemyLoot loot = new EnemyLoot()
            {
                EnemyId = enemyId,
                ItemId = itemId
            };

            _context.Entry(loot).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Remove an item from a enemy's loot
        /// </summary>
        /// <param name="enemyId">Id of enemy</param>
        /// <param name="itemId">Id of item</param>
        /// <returns>Successful result of item removal</returns>
        public async Task RemoveItemFromLoot(int enemyId, int itemId)
        {
            EnemyLoot result = await _context.EnemyLoot.FindAsync(enemyId, itemId);

            if (result != null)
            {
                _context.Entry(result).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get a list of a enemy's loot
        /// </summary>
        /// <param name="enemyId">Unique enemy ID</param>
        /// <returns>Successful result of list of items in loot</returns>
        public async Task<List<EnemyLootDTO>> GetEnemyLoot(int enemyId)
        {
            var result = await _context.EnemyLoot.Where(x => x.EnemyId == enemyId).Include(x => x.Item).ToListAsync();
            List<EnemyLootDTO> resultDTO = new List<EnemyLootDTO>();
            foreach (var item in result)
            {
                resultDTO.Add(new EnemyLootDTO()
                {
                    EnemyId = item.EnemyId,
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