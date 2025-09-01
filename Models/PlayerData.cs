using System;

namespace MooGameRefactorProject.Models
{
    public sealed class PlayerData : IEquatable<PlayerData>
    {
        public string Name { get; }
        public int NumberOfGames { get; private set; }
        public int TotalAttempts { get; private set; }

        public PlayerData(string name, int attempts)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            if (attempts < 0)
                throw new ArgumentOutOfRangeException(nameof(attempts), "Attempts cannot be negative.");

            Name = name;
            NumberOfGames = 1;
            TotalAttempts = attempts;
        }

        public void UpdateGame(int attempts)
        {
            if (attempts < 0)
                throw new ArgumentOutOfRangeException(nameof(attempts), "Attempts cannot be negative.");

            TotalAttempts += attempts;
            NumberOfGames++;
        }

        public double CalculateAverageAttempts()
            => (double)TotalAttempts / NumberOfGames;

        public bool Equals(PlayerData? other)
            => other is not null &&
               string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);

        public override bool Equals(object? obj) => Equals(obj as PlayerData);

        public override int GetHashCode()
            => StringComparer.OrdinalIgnoreCase.GetHashCode(Name);
    }
}
