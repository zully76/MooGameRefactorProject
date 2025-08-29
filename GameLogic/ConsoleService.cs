using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MooGameRefactorProject.GameLogic
{
    public class ConsoleService
    {
        public string GetUserName()
        {
            Console.WriteLine("Enter your user name: \n");
            return Console.ReadLine();
        }

        public bool IsValidInput(string input)
        {
            // Validamos si la entrada es nula o vacía
            if (string.IsNullOrEmpty(input))
                return false; // Si es nulo o vacío, retornamos false

            // Verificamos si la longitud es 4 y si contiene solo dígitos
            return input.Length == 4 && input.All(char.IsDigit);
        }
    }
}
