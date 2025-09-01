using System.Linq;

namespace MooGameRefactorProject.GameLogic
{
    public class GameManager
    {
        private readonly IRandomProvider _rng;

        // 👇 Ahora recibe la dependencia por constructor
        public GameManager(IRandomProvider rng)
        {
            _rng = rng;
        }

        public string GenerateNumber()
        {
            string correctNumber = string.Empty;

            while (correctNumber.Length < 4)
            {
                int randomNumber = _rng.Next(10); // usamos la interfaz
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
            // protección contra null o input corto
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
