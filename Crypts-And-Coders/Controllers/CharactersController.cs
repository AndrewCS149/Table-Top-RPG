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
    [Route("api/[controller]")]
    [Authorize(Policy = "AllUsers")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacter _character;
        private readonly ILog _log;

        public CharactersController(ICharacter character, ILog log)
        {
            _character = character;
            _log = log;
        }

        // GET: api/Characters
        [HttpGet]
        [Authorize(Policy = "GameMaster")]
        public async Task<ActionResult<IEnumerable<CharacterDTO>>> GetCharacters()
        {
            return await _character.GetCharacters();
        }

        // GET: api/Characters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDTO>> GetCharacter(int id)
        {
            var character = await _character.GetCharacter(id);

            if (!ValidateUser(User, _character, id))
            {
                return BadRequest("You do not have access to this account");
            }

            if (character == null)
            {
                return NotFound();
            }

            return character;
        }

        // PUT: api/Characters/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{charId}")]
        public async Task<IActionResult> PutCharacter(int charId, CharacterDTO character)
        {
            if (!ValidateUser(User, _character, charId))
            {
                return BadRequest("You do not have access to this account");
            }

            character.UserName = User.FindFirst("UserName").Value;

            if (charId != character.Id)
            {
                return BadRequest();
            }

            var result = await _character.Update(character);

            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            return Ok(result);
        }

        // POST: api/Characters
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Character>> PostCharacter(CharacterDTO character)
        {
            character.UserName = User.FindFirst("UserName").Value;

            await _character.Create(character);

            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            return CreatedAtAction("GetCharacter", new { id = character.Id }, character);
        }

        // DELETE: api/Characters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Character>> DeleteCharacter(int id)
        {
            if (!ValidateUser(User, _character, id))
            {
                return BadRequest("You do not have access to this account");
            }

            bool result = await _character.Delete(id);

            if (!result) return NotFound();

            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            return NoContent();
        }

        // POST: api/Characters/5/Items/1
        [HttpPost("{charId}/Items/{itemId}")]
        public async Task AddItemToInventory(int charId, int itemId)
        {
            if (ValidateUser(User, _character, charId))
            {
                await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);
                await _character.AddItemToInventory(charId, itemId);
            }
        }

        // DELETE: api/Characters/5/Items/1
        [HttpDelete("{charId}/Items/{itemId}")]
        public async Task DeleteItemFromInventory(int charId, int itemId)
        {
            if (ValidateUser(User, _character, charId))
            {
                await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);
                await _character.RemoveItemFromInventory(charId, itemId);
            }
        }
    }
}