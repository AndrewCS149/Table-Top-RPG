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
    public class ItemsController : ControllerBase
    {
        private readonly IItem _item;
        private readonly ILog _log;

        public ItemsController(IItem item, ILog log)
        {
            _item = item;
            _log = log;
        }

        // GET: api/Items
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetItems()
        {
            return await _item.GetItems();
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ItemDTO>> GetItem(int id)
        {
            var item = await _item.GetItem(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, ItemDTO item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            var result = await _item.Update(item);

            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            return Ok(result);
        }

        // POST: api/Items
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ItemDTO>> PostItem(ItemDTO item)
        {
            await _item.Create(item);

            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            return CreatedAtAction("GetItem", new { id = item.Id }, item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemDTO>> DeleteItem(int id)
        {
            await _item.Delete(id);

            await _log.CreateLog(HttpContext, User.FindFirst("UserName").Value);

            return NoContent();
        }
    }
}