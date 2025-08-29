using MooGameRefactorProject.Models;
using System.Collections.Generic;

namespace MooGameRefactorProject.Services
{
    public interface IFileService
    {
        void SaveResult(string playerName, int attempts);
        List<PlayerData> ReadPlayerResults();
        void ShowPlayerTopList();
    }
}
