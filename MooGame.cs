using System;
using System.Linq;
using MooGameRefactorProject.GameLogic;
using MooGameRefactorProject.Services;
using MooGameRefactorProject.Models;
using System.IO;

namespace MooGame
{
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
            bool isPlaying = true;
            string? userName = _consoleService.GetUserName();

            while (isPlaying)
            {
                PlayGame(userName);

                _consoleService.DisplayMessage("Do you want to continue? (y/n)");
                string? playerAnswer = _consoleService.GetUserInput("");

                if (playerAnswer != null && playerAnswer.Length > 0 && playerAnswer.Substring(0, 1).ToLower() == "n")
                {
                    isPlaying = false;
                }
            }
            _consoleService.DisplayMessage("Thanks for playing!");
        }

        private void PlayGame(string userName)
        {
            string correctNumber = _gameManager.GenerateNumber();
            _consoleService.DisplayMessage("New game started!:\n");
            _consoleService.DisplayMessage("For practice, number is: " + correctNumber + "\n");

            string? attempt;
            int numberOfAttempts = 0;
            string result = "";

            do
            {
                attempt = _consoleService.GetUserInput("Enter your guess (4 digits): ");

                if (string.IsNullOrEmpty(attempt) || !_consoleService.IsValidInput(attempt))
                {
                    _consoleService.DisplayMessage("Invalid input! Please enter exactly 4 digits.");
                    continue;
                }
                numberOfAttempts++;
                result = _gameManager.CheckGuess(correctNumber, attempt);
                _consoleService.DisplayMessage(result);
            }
            while (result != "BBBB,");

            _fileService.SaveResult(userName, numberOfAttempts);
            _fileService.ShowPlayerTopList();
            _consoleService.DisplayMessage("Correct, it took " + numberOfAttempts + " attempts.");
        }
    }
}
