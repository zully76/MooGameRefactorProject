using Microsoft.Extensions.DependencyInjection;
using MooGameRefactorProject.GameLogic;
using MooGameRefactorProject.Services;

namespace MooGame
{
    class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();

            // Registro de dependencias
            services.AddSingleton<ConsoleService>();
            services.AddSingleton<IRandomProvider, SystemRandomProvider>();
            services.AddSingleton<GameManager>();
            services.AddSingleton<IFileHandler, FileHandler>();
            services.AddSingleton<IFileService>(provider =>
                new FileService("results.txt", provider.GetRequiredService<IFileHandler>()));
            services.AddSingleton<MooGame>();

            var serviceProvider = services.BuildServiceProvider();

            // Ejecutar juego
            var mooGame = serviceProvider.GetRequiredService<MooGame>();
            mooGame.Run();
        }
    }
}
