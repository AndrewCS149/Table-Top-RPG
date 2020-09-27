using Crypts_And_Coders.Data;
using Crypts_And_Coders.Models.DTOs;
using Crypts_And_Coders.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crypts_And_Coders.Models.Services
{
    public class LocationsRepository : ILocation
    {
        private CryptsDbContext _context;
        private readonly IEnemy _enemy;

        public LocationsRepository(CryptsDbContext context, IEnemy enemy)
        {
            _context = context;
            _enemy = enemy;
        }

        /// <summary>
        /// Creates a new location in the database
        /// </summary>
        /// <param name="location">Location information for creation</param>
        /// <returns>Successful result of location creation</returns>
        public async Task<LocationDTO> Create(LocationDTO location)
        {
            Location entity = new Location()
            {
                Name = location.Name,
                Description = location.Description
            };

            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            location.Id = entity.Id;
            return location;
        }

        /// <summary>
        /// Delete a location from the database
        /// </summary>
        /// <param name="id">Id of location to be deleted</param>
        /// <returns>Task of completion for location delete</returns>
        public async Task Delete(int id)
        {
            Location location = await _context.Location.FindAsync(id);
            if (location != null)
            {
                _context.Entry(location).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get a list of all locations in the database
        /// </summary>
        /// <returns>Successful result with list of locations</returns>
        public async Task<List<LocationDTO>> GetLocations()
        {
            List<Location> result = await _context.Location.Include(x => x.Enemies).ToListAsync();
            List<LocationDTO> locationDTO = new List<LocationDTO>();

            foreach (var item in result)
            {
                locationDTO.Add(await GetLocation(item.Id));
            }

            return locationDTO;
        }

        /// <summary>
        /// Get a specific location in the database by ID
        /// </summary>
        /// <param name="id">Id of location to search for</param>
        /// <returns>Successful result of specified location</returns>
        public async Task<LocationDTO> GetLocation(int id)
        {
            var result = await _context.Location.Where(x => x.Id == id)
                                            .Include(x => x.Enemies)
                                            .FirstOrDefaultAsync();

            LocationDTO dto = new LocationDTO()
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description
            };

            dto.Enemies = new List<LocationEnemyInfoDTO>();
            foreach (var enemy in result.Enemies)
            {
                if (_enemy != null)
                {
                    var rawEnemy = await _enemy.GetEnemy(enemy.EnemyId);

                    dto.Enemies.Add(new LocationEnemyInfoDTO()
                    {
                        Id = rawEnemy.Id,
                        Abilities = rawEnemy.Abilities,
                        Species = rawEnemy.Species,
                        Type = rawEnemy.Type
                    });
                }
            }
            return dto;
        }

        /// <summary>
        /// Update a given location in the database
        /// </summary>
        /// <param name="id">Id of location to be updated</param>
        /// <param name="location">Location information for update</param>
        /// <returns>Successful result of specified updated location</returns>
        public async Task<LocationDTO> Update(LocationDTO locationDTO)
        {
            Location location = new Location()
            {
                Name = locationDTO.Name,
                Id = locationDTO.Id,
                Description = locationDTO.Description
            };
            _context.Entry(location).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return locationDTO;
        }

        /// <summary>
        /// Add an enemy to a location
        /// </summary>
        /// <param name="locationId">Id of location</param>
        /// <param name="enemyId">Id of enemy</param>
        /// <returns>Successful result of enemy addition</returns>
        public async Task AddEnemyToLocation(int locationId, int enemyId)
        {
            EnemyInLocation enemiesInLoc = new EnemyInLocation()
            {
                LocationId = locationId,
                EnemyId = enemyId
            };

            _context.Entry(enemiesInLoc).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Remove an enemy from a location
        /// </summary>
        /// <param name="locationId">Id of location</param>
        /// <param name="enemyId">Id of enemy</param>
        /// <returns>Successful result of enemy removal</returns>
        public async Task RemoveEnemyFromLocation(int locationId, int enemyId)
        {
            EnemyInLocation result = await _context.EnemyInLocation.FindAsync(locationId, enemyId);

            if (result != null)
            {
                _context.Entry(result).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }
    }
}