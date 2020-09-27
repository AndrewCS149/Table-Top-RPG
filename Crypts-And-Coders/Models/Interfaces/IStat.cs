using Crypts_And_Coders.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crypts_And_Coders.Models.Interfaces
{
    public interface IStat
    {
        /// <summary>
        /// Creates a new stat in the database
        /// </summary>
        /// <param name="stat">Stat information for creation</param>
        /// <returns>Successful result of stat creation</returns>
        Task<StatDTO> Create(StatDTO stat);

        /// <summary>
        /// Get a list of all stats in the database
        /// </summary>
        /// <returns>Successful result with list of stats</returns>
        Task<List<StatDTO>> GetStats();

        /// <summary>
        /// Get a specific stat in the database by ID
        /// </summary>
        /// <param name="id">Id of stat to search for</param>
        /// <returns>Successful result of specified stat</returns>
        Task<StatDTO> GetStat(int id);

        /// <summary>
        /// Update a given stat in the database
        /// </summary>
        /// <param name="id">Id of stat to be updated</param>
        /// <param name="stat">Stat information for update</param>
        /// <returns>Successful result of specified updated stat</returns>
        Task<StatDTO> Update(StatDTO stat);

        /// <summary>
        /// Delete a stat from the database
        /// </summary>
        /// <param name="id">Id of stat to be deleted</param>
        /// <returns>Task of completion for stat delete</returns>
        Task Delete(int id);
    }
}