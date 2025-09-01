using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooGameRefactorProject.GameLogic
{
    public class SystemRandomProvider : IRandomProvider
    {
        private readonly Random _random = new Random();
        public int Next(int maxExclusive) => _random.Next(maxExclusive);
            
    }
}
