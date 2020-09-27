using Crypts_And_Coders.Models;
using Crypts_And_Coders.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crypts_And_Coders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "GameMaster")]
    public class WeaponsController : ControllerBase
    {
        private readonly IWeapon _weapon;
        private readonly ILog _log;

        public WeaponsController(IWeapon weapon, ILog log)
        {
            _weapon = weapon;
            _log = log;
        }

        // GET: api/Weapons
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Weapon>>> GetWeapons()
        {
            return await _weapon.GetWeapons();
        }

        // GET: api/Weapons/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Weapon>> GetWeapon(int id)
        {
            var weapon = await _weapon.GetWeapon(id);

            if (weapon == null)
            {
                return NotFound();
            }

            return weapon;
        }

        // PUT: api/Weapons/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeapon(int id, Weapon weapon)
        {
            if (id != weapon.Id)
            {
                return BadRequest();
            }
            var result = await _weapon.Update(weapon);

            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            return Ok(result);
        }

        // POST: api/Weapons
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Weapon>> PostWeapon(Weapon weapon)
        {
            await _weapon.Create(weapon);

            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            return CreatedAtAction("GetWeapon", new { id = weapon.Id }, weapon);
        }

        // DELETE: api/Weapons/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Weapon>> DeleteWeapon(int id)
        {
            await _weapon.Delete(id);

            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            return NoContent();
        }
    }
}