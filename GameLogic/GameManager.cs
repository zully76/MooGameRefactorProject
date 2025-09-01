using System.Linq;

namespace MooGameRefactorProject.GameLogic
{
    /// <summary>
    /// Encapsulates Moo rules: generates a 4-unique-digit secret and evaluates Bulls/Cows.
    /// </summary>


    public class GameManager
    {
        private readonly IRandomProvider _rng;

        public GameManager(IRandomProvider rng)
        {
            _rng = rng;
        }

        public string GenerateNumber()
        {
            string correctNumber = string.Empty;

            while (correctNumber.Length < 4)
            {
                int randomNumber = _rng.Next(10);
                char digit = (char)('0' + randomNumber);

                if (!correctNumber.Contains(digit))
                {
                    correctNumber += digit;
                }
            }

            return correctNumber;
        }

        public string CheckGuess(string correctNumber, string attempt)
        {
            attempt = (attempt ?? string.Empty).PadRight(4).Substring(0, 4);

            int bulls = 0;
            int cows = 0;

            for (int i = 0; i < 4; i++)
            {
                if (attempt[i] == correctNumber[i])
                {
                    bulls++;
                }
                else if (correctNumber.Contains(attempt[i]))
                {
                    cows++;
                }
            }

            return new string('B', bulls) + "," + new string('C', cows);
        }
    }
}
