using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Crypts_And_Coders.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Crypts_And_Coders.Controllers
{
    [AllowAnonymous]
    public class LogViewController : Controller
    {
        private readonly ILog _log;

        public LogViewController(ILog log)
        {
            _log = log;
        }

        // GET: LogView
        public async Task<IActionResult> Index()
        {
            var logs = await _log.GetAllLogData();
            return View(logs);
        }

        // DELETE: LogView/Delete/5
        [AllowAnonymous]
        public async Task<IActionResult> Delete(int id)
        {
            var logData = await _log.DeleteLog(id);

            if (logData == null)
            {
                return NotFound();
            }

            return View(logData);
        }

        // POST: LogView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _log.DeleteLog(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
