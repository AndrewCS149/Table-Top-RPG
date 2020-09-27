using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crypts_And_Coders.Models.Interfaces
{
    public interface ILog
    {
        /// <summary>
        /// Return a specific log
        /// </summary>
        /// <param name="id">Unique id of log</param>
        /// <returns>Successful result of log data</returns>
        Task<LogData> GetLogData(int id);

        /// <summary>
        /// Return list of all logs
        /// </summary>
        /// <returns>Successful result of list of log data</returns>
        Task<List<LogData>> GetAllLogData();

        /// <summary>
        /// Delete a log from the database
        /// </summary>
        /// <param name="id">Id of log to be deleted</param>
        /// <returns>Log data of deleted log</returns>
        Task<LogData> DeleteLog(int id);

        /// <summary>
        /// Create a new log
        /// </summary>
        /// <param name="context">Http context for current action</param>
        /// <returns>Successful result of log data</returns>
        Task<LogData> CreateLog(HttpContext context, string userName);

    }
}