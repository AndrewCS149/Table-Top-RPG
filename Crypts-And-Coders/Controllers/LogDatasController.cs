using Crypts_And_Coders.Models;
using Crypts_And_Coders.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crypts_And_Coders.Controllers
{
    [Route("api/Logs")]
    [ApiController]
    public class LogDatasController : ControllerBase
    {
        private readonly ILog _log;

        public LogDatasController(ILog log)
        {
            _log = log;
        }

        // GET: api/Logs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogData>>> GetLogs()
        {
            return await _log.GetAllLogData();
        }

        // GET: api/Logs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LogData>> GetLogData(int id)
        {
            var logData = await _log.GetLogData(id);

            if (logData == null)
            {
                return NotFound();
            }

            return logData;
        }

        // DELETE: api/Logs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Stat>> DeleteStat(int id)
        {
            LogData result = await _log.DeleteLog(id);

            if(result != null)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}