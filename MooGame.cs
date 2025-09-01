using System;
using MooGameRefactorProject.GameLogic;
using MooGameRefactorProject.Services;

namespace MooGame
{

    /// <summary>
    /// Console loop: orchestrates game rounds and delegates logic/persistence to services.
    /// </summary>



    public class MooGame
    {
        private readonly ConsoleService _consoleService;
        private readonly GameManager _gameManager;
        private readonly IFileService _fileService;

        public MooGame(ConsoleService consoleService, GameManager gameManager, IFileService fileService)
        {
            _consoleService = consoleService;
            _gameManager = gameManager;
            _fileService = fileService;
        }

        public void Run()
        {
            string? userName = _consoleService.GetUserName();
            if (string.IsNullOrWhiteSpace(userName))
                userName = "Player";

            bool isPlaying = true;

            while (isPlaying)
            {
                PlayGame(userName);

                _consoleService.DisplayMessage("Do you want to continue? (y/n)");
                string? playerAnswer = _consoleService.GetUserInput("");

                if (IsExitAnswer(playerAnswer))
                {
                    isPlaying = false;
                }
            }

            _consoleService.DisplayMessage("Thanks for playing!");
        }

        private void PlayGame(string userName)
        {
            string correctNumber = _gameManager.GenerateNumber();
            _consoleService.DisplayMessage("New game started!");
            _consoleService.DisplayMessage($"For practice, number is: {correctNumber}");

            int numberOfAttempts = 0;
            string result;

            do
            {
                string attempt = GetValidAttempt();
                numberOfAttempts++;

                result = _gameManager.CheckGuess(correctNumber, attempt);
                _consoleService.DisplayMessage(result);
            }
            while (result != "BBBB,");

            _fileService.SaveResult(userName, numberOfAttempts);

            var lines = _fileService.GetTopListLines();
            foreach (var line in lines)
            {
                _consoleService.DisplayMessage(line);
            }

            _consoleService.DisplayMessage($"Correct, it took {numberOfAttempts} attempts.");
        }

        private string GetValidAttempt()
        {
            string? attempt;

            do
            {
                attempt = _consoleService.GetUserInput("Enter your guess (4 digits): ");

                if (string.IsNullOrEmpty(attempt) || !_consoleService.IsValidInput(attempt))
                {
                    _consoleService.DisplayMessage("Invalid input! Please enter exactly 4 digits.");
                    attempt = null;
                }
            }
            while (attempt == null);

            return attempt;
        }

        private bool IsExitAnswer(string? answer)
        {
            return !string.IsNullOrEmpty(answer) &&
                   answer.StartsWith("n", StringComparison.OrdinalIgnoreCase);
        }
    }
}
