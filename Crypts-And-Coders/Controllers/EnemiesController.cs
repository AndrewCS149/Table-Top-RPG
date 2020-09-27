using Crypts_And_Coders.Models;
using Crypts_And_Coders.Models.DTOs;
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
    public class EnemiesController : ControllerBase
    {
        private readonly IEnemy _enemy;
        private readonly ILog _log;

        public EnemiesController(IEnemy enemy, ILog log)
        {
            _enemy = enemy;
            _log = log;
        }

        // GET: api/Enemies
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<EnemyDTO>>> GetEnemies()
        {
            return await _enemy.GetEnemies();
        }

        // GET: api/Enemies/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<EnemyDTO>> GetEnemy(int id)
        {
            var enemy = await _enemy.GetEnemy(id);

            if (enemy == null)
            {
                return NotFound();
            }

            return enemy;
        }

        // PUT: api/Enemies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnemy(int id, EnemyDTO enemy)
        {
            if (id != enemy.Id)
            {
                return BadRequest();
            }
            var result = await _enemy.Update(enemy);

            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            return Ok(result);
        }

        // POST: api/Enemies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EnemyDTO>> PostEnemy(EnemyDTO enemy)
        {
            await _enemy.Create(enemy);

            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            return CreatedAtAction("GetEnemy", new { id = enemy.Id }, enemy);
        }

        // DELETE: api/Enemies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Enemy>> DeleteEnemy(int id)
        {
            await _enemy.Delete(id);

            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            return NoContent();
        }

        // POST: api/Characters/5/Items/1
        [HttpPost("{enemyId}/Loot/{itemId}")]
        public async Task AddItemToLoot(int enemyId, int itemId)
        {
            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            await _enemy.AddItemToLoot(enemyId, itemId);
        }

        // DELETE: api/Characters/5/Items/1
        [HttpDelete("{enemyId}/Loot/{itemId}")]
        public async Task DeleteItemFromInventory(int enemyId, int itemId)
        {
            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            await _enemy.RemoveItemFromLoot(enemyId, itemId);
        }
    }
}