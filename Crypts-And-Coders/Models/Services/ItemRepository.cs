using Crypts_And_Coders.Data;
using Crypts_And_Coders.Models.DTOs;
using Crypts_And_Coders.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crypts_And_Coders.Models.Services
{
    public class ItemRepository : IItem
    {
        private CryptsDbContext _context;

        public ItemRepository(CryptsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new item in the database
        /// </summary>
        /// <param name="item">Item information for creation</param>
        /// <returns>Successful result of item creation</returns>
        public async Task<ItemDTO> Create(ItemDTO itemDTO)
        {
            Item item = new Item()
            {
                Name = itemDTO.Name,
                Value = itemDTO.Value
            };
            _context.Entry(item).State = EntityState.Added;
            await _context.SaveChangesAsync();
            itemDTO.Id = item.Id;
            return itemDTO;
        }

        /// <summary>
        /// Delete a item from the database
        /// </summary>
        /// <param name="id">Id of item to be deleted</param>
        /// <returns>Task of completion for item delete</returns>
        public async Task Delete(int id)
        {
            Item item = await _context.Item.FindAsync(id);
            if (item != null)
            {
                _context.Entry(item).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get a list of all items in the database
        /// </summary>
        /// <returns>Successful result with list of items</returns>
        public async Task<List<ItemDTO>> GetItems()
        {
            List<Item> result = await _context.Item.ToListAsync();
            List<ItemDTO> resultDTO = new List<ItemDTO>();
            foreach (var item in result)
            {
                resultDTO.Add(await GetItem(item.Id));
            }
            return resultDTO;
        }

        /// <summary>
        /// Get a specific item in the database by ID
        /// </summary>
        /// <param name="id">Id of item to search for</param>
        /// <returns>Successful result of specified item</returns>
        public async Task<ItemDTO> GetItem(int id)
        {
            var result = await _context.Item.Where(x => x.Id == id).FirstOrDefaultAsync();
            ItemDTO resultDTO = new ItemDTO()
            {
                Id = result.Id,
                Name = result.Name,
                Value = result.Value
            };
            return resultDTO;
        }

        /// <summary>
        /// Update a given location in the database
        /// </summary>
        /// <param name="id">Id of item to be updated</param>
        /// <param name="item">Item information for update</param>
        /// <returns>Successful result of specified updated item</returns>
        public async Task<ItemDTO> Update(ItemDTO itemDTO)
        {
            Item item = new Item()
            {
                Id = itemDTO.Id,
                Name = itemDTO.Name,
                Value = itemDTO.Value
            };
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return itemDTO;
        }
    }
}