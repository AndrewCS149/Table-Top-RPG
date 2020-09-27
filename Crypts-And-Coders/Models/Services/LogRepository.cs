using Crypts_And_Coders.Data;
using Crypts_And_Coders.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.WebApiCompatShim;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Crypts_And_Coders.Models.Services
{
    public class LogRepository : ILog
    {
        private readonly CryptsDbContext _context;

        public LogRepository(CryptsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Return a specific log
        /// </summary>
        /// <param name="id">Unique id of log</param>
        /// <returns>Successful result of log data</returns>
        public async Task<LogData> GetLogData(int id)
        {
            var result = await _context.Logs.Where(x => x.Id == id).FirstOrDefaultAsync();
            return result;
        }

        /// <summary>
        /// Return list of all logs
        /// </summary>
        /// <returns>Successful result of list of log data</returns>
        public async Task<List<LogData>> GetAllLogData()
        {
            var result = await _context.Logs.ToListAsync();
            return result;
        }

        /// <summary>
        /// Create a new log
        /// </summary>
        /// <param name="context">Http context for current action</param>
        /// <returns>Successful result of log data</returns>
        public async Task<LogData> CreateLog(HttpContext context, string userName)
        {
            // Get message from context
            HttpRequestMessageFeature hreqmf = new HttpRequestMessageFeature(context);
            HttpRequestMessage req = hreqmf.HttpRequestMessage;
            DateTime dt = DateTime.Now;
            LogData log = new LogData()
            {
                Message = $"{req.Method.Method} call made by {userName} to {req.RequestUri.ToString()} on {String.Format("{0:ddd, MMM d, yyyy}", dt)} at {String.Format("{0:t}", dt)}."
            };
            _context.Entry(log).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return log;
        }

        public async Task<LogData> DeleteLog(int id)
        {
            LogData log = await _context.Logs.FindAsync(id);
            if (log != null)
            {
                _context.Entry(log).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return log;
            }
            return null;
        }
    }
}