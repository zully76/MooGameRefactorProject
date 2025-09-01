using MooGameRefactorProject.Models;
using System.Collections.Generic;

namespace MooGameRefactorProject.Services
{
    public interface IFileService
    {
        void SaveResult(string playerName, int attempts);
        List<PlayerData> ReadPlayerResults();

        /// <summary>
        /// Returns pre-formatted leaderboard lines; UI is responsible for printing.
        /// </summary>
        /// 
        List<string> GetTopListLines();
    }
}
