using System;
using System.Linq;
using MooGameRefactorProject.GameLogic;
using MooGameRefactorProject.Services;
using MooGameRefactorProject.Models;
using System.IO;
namespace MooGame
{
    class MainClass
    {

        public static void Main(string[] args)
        {
            ConsoleService consoleService = new ConsoleService();
            GameManager gameManager = new GameManager();
            IFileHandler fileHandler = new FileHandler();
            FileService fileService = new FileService("results.txt", fileHandler);

            bool isPlaying = true;
            string? userName = consoleService.GetUserName(); //aqui llamamos al metodo GetUserName
   
            

            while (isPlaying)
            {
                PlayGame(userName, consoleService, gameManager, fileService);    //llamamos al nuevo metodo PlayGame para manejar toda la logica


                Console.WriteLine("Do you want to continue? (y/n)");
                string playerAnswer = Console.ReadLine();

                if (playerAnswer != null && playerAnswer.Length > 0 && playerAnswer.Substring(0, 1) == "n")
                {
                    isPlaying = false;
                }

            }
            Console.WriteLine("Thanks for playing!");
        }

        //metodo PlayGame que maneja la logica del juego
        public static void PlayGame(string userName, ConsoleService consoleService, GameManager gameManager, FileService fileService)
        {
           
            string correctNumber = gameManager.GenerateNumber();
            Console.WriteLine("New game started!:\n");
            //comment out or remove next line to play real games!
            Console.WriteLine("For practice, number is: " + correctNumber + "\n");

            string attempt;
            int numberOfAttempts = 0;
            string result = "";

            do
            {
                Console.Write("Enter your guess (4 digits): ");
                attempt = Console.ReadLine();

                if (!consoleService.IsValidInput(attempt))
                {
                    Console.WriteLine("Invalid input! Please enter exactly 4 digits.");
                    continue;
                }
                numberOfAttempts++;
                // Usamos el objeto gameManager para verificar la suposición
                result = gameManager.CheckGuess(correctNumber, attempt);
                Console.WriteLine(result);
            }
            while (result != "BBBB,");  // continuar hasta adivinar correctamente

            // Usamos el objeto fileService para guardar el resultado
            fileService.SaveResult(userName, numberOfAttempts);

            // Usamos el objeto fileService para mostrar la lista
            fileService.ShowPlayerTopList();

            Console.WriteLine("Correct, it took " + numberOfAttempts + " attempts.");
           

        }
    }
}
        /*
        public static bool IsValidInput(string input)
        {
            return input.Length == 4 && input.All(char.IsDigit);
        }
    }
}

        //METODO NUEVO PARA PEDIR ELNOMBRE DEL JUGADOR SIN CAMBIAR EL FLUJO NI LOGICA//
         /* public static string? GetUserName()
          {
              Console.WriteLine("Enter your user name:\n");
              return Console.ReadLine();
        }
         */
        // Métodos estáticos movidos a las nuevas clases
        // Se mantienen aquí por ahora según tu petición de no eliminar nada
        /*static string generateNumber()
        {
            Random randomNumberGenerator = new Random();
            string correctNumber = "";
            for (int i = 0; i < 4; i++)
            {
                int randomNumber = randomNumberGenerator.Next(10);
                string randomDigit = "" + randomNumber;
                while (correctNumber.Contains(randomDigit))
                {
                    randomNumber = randomNumberGenerator.Next(10);
                    randomDigit = "" + randomNumber;
                }
                correctNumber = correctNumber + randomDigit;
            }
            return correctNumber;
        }
        */
        /*
        static string getBullsAndCows(string correctNumber, string attempt)
        {
            int cows = 0, bulls = 0;
            attempt += "    ";       // if player entered less than 4 chars
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (correctNumber[i] == attempt[j])
                    {
                        if (i == j)
                        {
                            bulls++;
                        }
                        else
                        {
                            cows++;
                        }
                    }
                }
            }
            return "BBBB".Substring(0, bulls) + "," + "CCCC".Substring(0, cows);
        }
        */

        /*
        static void showPlayerTopList()
        {
            StreamReader resultTextReader = new StreamReader("result.txt");
            List<PlayerData> playerResults = new List<PlayerData>();
            string resultText;
            while ((resultText = resultTextReader.ReadLine()) != null)
            {
                string[] userNameAndScore = resultText.Split(new string[] { "#&#" }, StringSplitOptions.None);
                string userName = userNameAndScore[0];
                int attempts = Convert.ToInt32(userNameAndScore[1]);
                PlayerData pd = new PlayerData(userName, attempts);
                int pos = playerResults.IndexOf(pd);
                if (pos < 0)
                {
                    playerResults.Add(pd);
                }
                else
                {
                    playerResults[pos].updateGame(attempts);
                }
           }

}
            playerResults.Sort((player1, player2) => player1.CalculateAverageAttempts().CompareTo(player2.CalculateAverageAttempts()));
            Console.WriteLine("Player    games average");
            foreach (PlayerData p in playerResults)
            {
                Console.WriteLine(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.userName, p.numberOfGames, p.CalculateAverageAttempts()));
            }
            resultTextReader.Close();
        }
   /* }

    class PlayerData
    {
        public string userName { get; private set; }
        public int numberOfGames { get; private set; }
        int totalAttempts;


        public PlayerData(string userName, int attempts)
        {
            this.userName = userName;
            numberOfGames = 1;
            totalAttempts = attempts;
        }

        public void updateGame(int attempts)
        {
            totalAttempts += attempts;
            numberOfGames++;
        }

        public double CalculateAverageAttempts()
        {
            return (double)totalAttempts / numberOfGames;
        }


        public override bool Equals(Object? p)
        {
           
           if (p == null || ! (p is PlayerData))
            {
                return false;
            }

           return userName.Equals(((PlayerData)p).userName);

        }


        public override int GetHashCode()
        {
            return userName.GetHashCode();
        }
    }
}*/