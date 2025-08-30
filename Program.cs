using MooGameRefactorProject.GameLogic;
using MooGameRefactorProject.Services;
using MooGameRefactorProject.Models;

namespace MooGame
{
    class Program
    {
        public static void Main(string[] args)
        {
            ConsoleService consoleService = new ConsoleService();
            GameManager gameManager = new GameManager();
            IFileHandler fileHandler = new FileHandler();
            IFileService fileService = new FileService("results.txt", fileHandler);

            MooGame mooGame = new MooGame(consoleService, gameManager, fileService);
            mooGame.Run();
        }
    }
}
