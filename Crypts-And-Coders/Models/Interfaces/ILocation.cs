using Crypts_And_Coders.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crypts_And_Coders.Models.Interfaces
{
    public interface ILocation
    {
        /// <summary>
        /// Creates a new location in the database
        /// </summary>
        /// <param name="location">Location information for creation</param>
        /// <returns>Successful result of location creation</returns>
        Task<LocationDTO> Create(LocationDTO location);

        /// <summary>
        /// Get a list of all locations in the database
        /// </summary>
        /// <returns>Successful result with list of locations</returns>
        Task<List<LocationDTO>> GetLocations();

        /// <summary>
        /// Get a specific location in the database by ID
        /// </summary>
        /// <param name="id">Id of location to search for</param>
        /// <returns>Successful result of specified location</returns>
        Task<LocationDTO> GetLocation(int id);

        /// <summary>
        /// Update a given location in the database
        /// </summary>
        /// <param name="location">Location information for update</param>
        /// <returns>Successful result of specified updated location</returns>
        Task<LocationDTO> Update(LocationDTO location);

        /// <summary>
        /// Delete a location from the database
        /// </summary>
        /// <param name="id">Id of location to be deleted</param>
        /// <returns>Task of completion for location delete</returns>
        Task Delete(int id);

        /// <summary>
        /// Add an enemy to a location
        /// </summary>
        /// <param name="locationId">Id of location</param>
        /// <param name="enemyId">Id of enemy</param>
        /// <returns>Successful result of enemy addition</returns>
        Task AddEnemyToLocation(int locationId, int enemyId);

        /// <summary>
        /// Remove an enemy from a location
        /// </summary>
        /// <param name="locationId">Id of location</param>
        /// <param name="enemyId">Id of enemy</param>
        /// <returns>Successful result of enemy removal</returns>
        Task RemoveEnemyFromLocation(int locationId, int enemyId);
    }
}