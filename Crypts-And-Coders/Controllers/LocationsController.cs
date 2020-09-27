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
    public class LocationsController : ControllerBase
    {
        private readonly ILocation _location;
        private readonly ILog _log;

        public LocationsController(ILocation location, ILog log)
        {
            _location = location;
            _log = log;
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDTO>>> GetLocations()
        {
            return await _location.GetLocations();
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationDTO>> GetLocation(int id)
        {
            var location = await _location.GetLocation(id);

            if (location == null)
            {
                return NotFound();
            }

            return location;
        }

        // PUT: api/Locations/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, LocationDTO location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != location.Id)
            {
                return BadRequest();
            }

            await _location.Update(location);

            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            return NoContent();
        }

        // POST: api/Locations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation(LocationDTO location)
        {
            await _location.Create(location);

            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            return CreatedAtAction("GetLocation", new { id = location.Id }, location);
        }

        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Location>> DeleteLocation(int id)
        {
            await _location.Delete(id);

            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            return NoContent();
        }

        // POST: api/Characters/5/Enemys/1
        [HttpPost("{locationId}/Locations/{enemyId}")]
        public async Task AddEnemyToLocation(int locationId, int enemyId)
        {
            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            await _location.AddEnemyToLocation(locationId, enemyId);
        }

        // DELETE: api/Characters/5/Enemys/1
        [HttpDelete("{locationId}/Locations/{enemyId}")]
        public async Task DeleteEnemyFromInventory(int locationId, int enemyId)
        {
            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            await _location.RemoveEnemyFromLocation(locationId, enemyId);
        }

        // POST: api/Location/5/Enemies/1
        [HttpPost("{locationId}/Enemies/{enemyId}")]
        public async Task AddEnemyToLoot(int locationId, int enemyId)
        {
            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            await _location.AddEnemyToLocation(locationId, enemyId);
        }

        // DELETE: api/Locations/5/Enemies/1
        [HttpDelete("{locationId}/Enemies/{enemyId}")]
        public async Task DeleteEnemyFromLocation(int locationId, int enemyId)
        {
            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            await _location.RemoveEnemyFromLocation(locationId, enemyId);
        }
    }
}