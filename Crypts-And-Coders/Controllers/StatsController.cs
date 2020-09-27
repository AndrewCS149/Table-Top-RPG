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
    public class StatsController : ControllerBase
    {
        private readonly IStat _stat;
        private readonly ILog _log;

        public StatsController(IStat stat, ILog log)
        {
            _stat = stat;
            _log = log;
        }

        // GET: api/Stats
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<StatDTO>>> GetStats()
        {
            return await _stat.GetStats();
        }

        // GET: api/Stats/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<StatDTO>> GetStat(int id)
        {
            var stat = await _stat.GetStat(id);

            if (stat == null)
            {
                return NotFound();
            }

            return stat;
        }

        // PUT: api/Stats/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStat(int id, StatDTO stat)
        {
            if (id != stat.Id)
            {
                return BadRequest();
            }

            var result = await _stat.Update(stat);

            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            return Ok(result);
        }

        // POST: api/Stats
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<StatDTO>> PostStat(StatDTO stat)
        {
            await _stat.Create(stat);

            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            return CreatedAtAction("GetStat", new { id = stat.Id }, stat);
        }

        // DELETE: api/Stats/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Stat>> DeleteStat(int id)
        {
            await _stat.Delete(id);

            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            return NoContent();
        }
    }
}