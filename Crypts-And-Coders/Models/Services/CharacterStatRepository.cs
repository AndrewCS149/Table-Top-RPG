using Crypts_And_Coders.Data;
using Crypts_And_Coders.Models.DTOs;
using Crypts_And_Coders.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crypts_And_Coders.Models.Services
{
    public class CharacterStatRepository : ICharacterStat
    {
        private readonly CryptsDbContext _context;
        private readonly IStat _stat;

        public CharacterStatRepository(CryptsDbContext context, IStat stat)
        {
            _context = context;
            _stat = stat;
        }

        /// <summary>
        /// Creates a new characterStat in the database
        /// </summary>
        /// <param name="characterStat">CharacterStatDTO information for creation</param>
        /// <returns>Successful result of characterStat creation</returns>
        public async Task<CharacterStatDTO> Create(CharacterStatDTO characterStatDTO)
        {
            CharacterStat characterStat = new CharacterStat()
            {
                CharacterId = characterStatDTO.CharacterId,
                StatId = characterStatDTO.StatId,
                Level = characterStatDTO.Level
            };
            _context.Entry(characterStat).State = EntityState.Added;
            await _context.SaveChangesAsync();
            characterStatDTO.Stat = await _stat.GetStat(characterStatDTO.StatId);
            return characterStatDTO;
        }

        /// <summary>
        /// Update a given characterStat in the database
        /// </summary>
        /// <param name="id">Id of characterStat to be updated</param>
        /// <param name="characterStat">CharacterStatDTO information for update</param>
        /// <returns>Successful result of specified updated characterStat</returns>
        public async Task<CharacterStatDTO> Update(CharacterStatDTO characterStatDTO)
        {
            CharacterStat characterStat = new CharacterStat()
            {
                CharacterId = characterStatDTO.CharacterId,
                StatId = characterStatDTO.StatId,
                Level = characterStatDTO.Level
            };
            _context.Entry(characterStat).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            characterStatDTO.Stat = await _stat.GetStat(characterStatDTO.StatId);
            return characterStatDTO;
        }

        /// <summary>
        /// Delete a characterStat from the database
        /// </summary>
        /// <param name="id">Id of characterStat to be deleted</param>
        /// <returns>Task of completion for characterStat delete</returns>
        public async Task Delete(int charId, int statId)
        {
            CharacterStat characterStat = await _context.StatSheet.FindAsync(charId, statId);
            if (characterStat != null)
            {
                _context.Entry(characterStat).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get a specific characterStat in the database by ID
        /// </summary>
        /// <param name="id">Id of characterStat to search for</param>
        /// <returns>Successful result of specified characterStat</returns>
        public async Task<CharacterStatDTO> GetCharacterStat(int charId, int statId)
        {
            var result = await _context.StatSheet.Where(x => x.CharacterId == charId && x.StatId == statId).Include(x => x.Stat).Include(x => x.Character).FirstOrDefaultAsync();
            CharacterStatDTO resultDTO = new CharacterStatDTO()
            {
                CharacterId = result.CharacterId,
                StatId = result.StatId,
                Stat = new StatDTO()
                {
                    Name = result.Stat.Name,
                    Id = result.Stat.Id
                },
                Level = result.Level
            };

            return resultDTO;
        }

        /// <summary>
        /// Get a list of all characterStats in the database
        /// </summary>
        /// <returns>Successful result with list of characterStats</returns>
        public async Task<List<CharacterStatDTO>> GetCharacterStats(int id)
        {
            List<CharacterStat> result = await _context.StatSheet.Where(x => x.CharacterId == id).Include(x => x.Stat).ToListAsync();
            List<CharacterStatDTO> resultDTO = new List<CharacterStatDTO>();
            foreach (var item in result)
            {
                CharacterStatDTO newDTO = new CharacterStatDTO()
                {
                    CharacterId = item.CharacterId,
                    StatId = item.StatId,
                    Stat = new StatDTO()
                    {
                        Name = item.Stat.Name,
                        Id = item.Stat.Id
                    },
                    Level = item.Level
                };
                resultDTO.Add(newDTO);
            }
            return resultDTO;
        }
    }
}