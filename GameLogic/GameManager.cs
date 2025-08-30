using System;
using System.Linq;

namespace MooGameRefactorProject.GameLogic
{
    public class GameManager
    {
        public string GenerateNumber()
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

        public string CheckGuess(string correctNumber, string attempt)
        {
            int cows = 0;
            int bulls = 0;

            attempt += "    "; // Add spaces to handle attempts shorter than 4 digits

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
    }
}
