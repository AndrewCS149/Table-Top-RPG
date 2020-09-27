using Crypts_And_Coders.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crypts_And_Coders.Models.Interfaces
{
    public interface IItem
    {
        /// <summary>
        /// Creates a new item in the database
        /// </summary>
        /// <param name="item">item information for creation</param>
        /// <returns>Successful result of item creation</returns>
        Task<ItemDTO> Create(ItemDTO item);

        /// <summary>
        /// Get a list of all items in the database
        /// </summary>
        /// <returns>Successful result with list of items</returns>
        Task<List<ItemDTO>> GetItems();

        /// <summary>
        /// Get a specific item in the database by ID
        /// </summary>
        /// <param name="id">Id of item to search for</param>
        /// <returns>Successful result of specified item</returns>
        Task<ItemDTO> GetItem(int id);

        /// <summary>
        /// Update a given item in the database
        /// </summary>
        /// <param name="item">Item information for update</param>
        /// <returns>Successful result of specified updated item</returns>
        Task<ItemDTO> Update(ItemDTO itemDTO);

        /// <summary>
        /// Delete an item from the database
        /// </summary>
        /// <param name="id">Id of item to be deleted</param>
        /// <returns>Task of completion for item delete</returns>
        Task Delete(int id);
    }
}