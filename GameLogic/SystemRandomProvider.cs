using System;

namespace MooGameRefactorProject.GameLogic
{
    public class SystemRandomProvider : IRandomProvider
    {
        private readonly Random _random = new Random();

        public int Next(int maxExclusive) => _random.Next(maxExclusive);
    }
}
