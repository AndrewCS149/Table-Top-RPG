using Crypts_And_Coders.Models;
using Crypts_And_Coders.Models.DTOs;
using Crypts_And_Coders.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Crypts_And_Coders.Models.Services.UserServices;

namespace Crypts_And_Coders.Controllers
{
    [Route("api/Characters")]
    [Authorize(Policy = "AllUsers")]
    [ApiController]
    public class CharacterStatsController : ControllerBase
    {
        private readonly ICharacterStat _characterStat;
        private readonly ICharacter _character;
        private readonly ILog _log;

        public CharacterStatsController(ICharacterStat characterStat, ICharacter character, ILog log)
        {
            _characterStat = characterStat;
            _character = character;
            _log = log;
        }

        // GET: api/Characters/5/Stats
        [HttpGet("{charId}/Stats")]
        public async Task<ActionResult<IEnumerable<CharacterStatDTO>>> GetStatSheet(int charId)
        {
            if (!ValidateUser(User, _character, charId))
            {
                return BadRequest("You do not have access to this account");
            }
            return await _characterStat.GetCharacterStats(charId);
        }

        // GET: api/CharacterStats/5
        [HttpGet("{charId}/Stats/{statId}")]
        public async Task<ActionResult<CharacterStatDTO>> GetCharacterStat(int charId, int statId)
        {
            if (!ValidateUser(User, _character, charId))
            {
                return BadRequest("You do not have access to this account");
            }

            var characterStat = await _characterStat.GetCharacterStat(charId, statId);

            if (characterStat == null)
            {
                return NotFound();
            }

            return characterStat;
        }

        // PUT: api/Characters/5/Stats
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{charId}/Stats/{statId}")]
        public async Task<IActionResult> PutCharacterStat(int charId, int statId, CharacterStatDTO characterStat)
        {
            if (!ValidateUser(User, _character, charId))
            {
                return BadRequest("You do not have access to this account");
            }

            if (statId != characterStat.StatId || charId != characterStat.CharacterId)
            {
                return BadRequest();
            }

            await _characterStat.Update(characterStat);

            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            return NoContent();
        }

        // POST: api/Characters/5/Stats
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("{charId}/Stats")]
        public async Task<ActionResult<CharacterStat>> PostCharacterStat(int charId, CharacterStatDTO characterStat)
        {
            if (!ValidateUser(User, _character, charId))
            {
                return BadRequest("You do not have access to this account");
            }

            await _characterStat.Create(characterStat);

            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            return CreatedAtAction("GetCharacterStat", new { id = characterStat.StatId }, characterStat);
        }

        // DELETE: api/Characters/5/Stats/1
        [HttpDelete("{charId}/Stats/{statId}")]
        public async Task<ActionResult<CharacterStat>> DeleteCharacterStat(int charId, int statId)
        {
            if (!ValidateUser(User, _character, charId))
            {
                return BadRequest("You do not have access to this account");
            }

            await _characterStat.Delete(charId, statId);

            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            return NoContent();
        }
    }
}