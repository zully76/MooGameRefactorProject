using MooGameRefactorProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MooGameRefactorProject.Services
{
    public class FileService : IFileService
    {
        private const string Delimiter = "#&#";

        private readonly string _filePath;
        private readonly IFileHandler _fileHandler;

        public FileService(string filePath, IFileHandler fileHandler)
        {
            _filePath = filePath;
            _fileHandler = fileHandler;
        }

        public void SaveResult(string playerName, int attempts)
        {
            var lines = _fileHandler.ReadAllLines(_filePath).ToList();

            // busca línea existente exacta por nombre (case-sensitive aquí para no duplicar por formato exacto)
            var existingLine = lines.FirstOrDefault(line => line.StartsWith(playerName + Delimiter, StringComparison.Ordinal));

            if (existingLine != null)
            {
                var parts = existingLine.Split(new[] { Delimiter }, StringSplitOptions.None);
                int existingAttempts = 0;
                int.TryParse(parts.ElementAtOrDefault(1), out existingAttempts);
                int newAttempts = existingAttempts + attempts;

                lines[lines.IndexOf(existingLine)] = $"{playerName}{Delimiter}{newAttempts}";
            }
            else
            {
                lines.Add($"{playerName}{Delimiter}{attempts}");
            }

            _fileHandler.WriteAllLines(_filePath, lines);
        }

        public List<PlayerData> ReadPlayerResults()
        {
            var playerResults = new List<PlayerData>();

            if (!_fileHandler.Exists(_filePath))
            {
                return playerResults;
            }

            string[] lines = _fileHandler.ReadAllLines(_filePath);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(new[] { Delimiter }, StringSplitOptions.None);
                if (parts.Length < 2) continue;

                string playerName = parts[0];
                if (!int.TryParse(parts[1], out int attempts)) continue;

                // Unificar por nombre ignorando mayúsculas/minúsculas
                var existingPlayer = playerResults.FirstOrDefault(
                    p => p.Name.Equals(playerName, StringComparison.OrdinalIgnoreCase));

                if (existingPlayer != null)
                {
                    existingPlayer.UpdateGame(attempts);
                }
                else
                {
                    playerResults.Add(new PlayerData(playerName, attempts));
                }
            }

            return playerResults;
        }

        public List<string> GetTopListLines()
        {
            var playerResults = ReadPlayerResults();
            var output = new List<string>();

            if (playerResults.Count == 0)
            {
                output.Add("No results available.");
                return output;
            }

            // ordenar por promedio (menor es mejor)
            playerResults.Sort((a, b) => a.CalculateAverageAttempts().CompareTo(b.CalculateAverageAttempts()));

            output.Add("Player      games average");
            foreach (var player in playerResults)
            {
                // usa formato con cultura actual para el decimal (ej. 4,50 en sv-SE)
                output.Add(string.Format("{0,-9}{1,5:D}{2,9:F2}",
                    player.Name, player.NumberOfGames, player.CalculateAverageAttempts()));
            }

            return output;
        }
    }
}
