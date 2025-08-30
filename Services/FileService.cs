using MooGameRefactorProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MooGameRefactorProject.Services
{
    public class FileService : IFileService
    {
        private readonly string _filePath;
        private readonly IFileHandler _fileHandler;

        // Constructor de la clase FileService, recibe el archivo y el handler para manejar el archivo
        public FileService(string filePath, IFileHandler fileHandler)
        {
            _filePath = filePath;
            _fileHandler = fileHandler;
        }

        // Método para guardar el resultado de un jugador en el archivo
        public void SaveResult(string playerName, int attempts)
        {
            // Leer el contenido del archivo
            var lines = _fileHandler.ReadAllLines(_filePath).ToList();

            // Buscar si el jugador ya existe
            var existingLine = lines.FirstOrDefault(line => line.StartsWith(playerName + "#&#"));

            if (existingLine != null)
            {
                // Si el jugador ya existe, actualizamos el número de intentos
                var parts = existingLine.Split(new string[] { "#&#" }, StringSplitOptions.None);
                int existingAttempts = int.Parse(parts[1]);
                int newAttempts = existingAttempts + attempts;

                // Reemplazar la línea existente con el nuevo número de intentos
                lines[lines.IndexOf(existingLine)] = $"{playerName}#&#{newAttempts}";
            }
            else
            {
                // Si el jugador no existe, agregamos una nueva línea
                lines.Add($"{playerName}#&#{attempts}");
            }

            // Guardar el contenido actualizado en el archivo
            _fileHandler.WriteAllLines(_filePath, lines);
        }

        // Método para leer los resultados de los jugadores desde el archivo
        public List<PlayerData> ReadPlayerResults()
        {
            List<PlayerData> playerResults = new List<PlayerData>();

            // Verificamos si el archivo existe
            if (!_fileHandler.Exists(_filePath))
            {
                return playerResults;
            }

            // Leemos todas las líneas del archivo
            string[] lines = _fileHandler.ReadAllLines(_filePath);

            foreach (var line in lines)
            {
                // Dividimos cada línea por el delimitador "#&#"
                string[] parts = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
                if (parts.Length < 2) continue; // Si la línea no tiene los datos esperados, la ignoramos

                string playerName = parts[0];
                int attempts = Convert.ToInt32(parts[1]);

                // Buscamos si el jugador ya existe en la lista
                var existingPlayer = playerResults.FirstOrDefault(p => p.Name.Equals(playerName, StringComparison.OrdinalIgnoreCase));

                if (existingPlayer != null)
                {
                    // Si el jugador ya existe, actualizamos sus datos
                    existingPlayer.UpdateGame(attempts);
                }
                else
                {
                    // Si el jugador no existe, lo agregamos a la lista
                    playerResults.Add(new PlayerData(playerName, attempts));
                }
            }

            return playerResults;
        }

        // Método para mostrar los mejores jugadores ordenados por el promedio de intentos
        public void ShowPlayerTopList()
        {
            List<PlayerData> playerResults = ReadPlayerResults();

            // Si no hay resultados, mostramos un mensaje
            if (playerResults.Count == 0)
            {
                Console.WriteLine("No results available.");
                return;
            }

            // Ordenamos a los jugadores por su promedio de intentos
            playerResults.Sort((player1, player2) => player1.CalculateAverageAttempts().CompareTo(player2.CalculateAverageAttempts()));

            // Mostramos los resultados
            Console.WriteLine("Player      games average");
            foreach (var player in playerResults)
            {
                Console.WriteLine(string.Format("{0,-9}{1,5:D}{2,9:F2}", player.Name, player.NumberOfGames, player.CalculateAverageAttempts()));
            }
        }
    }
}
