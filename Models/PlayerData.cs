using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooGameRefactorProject.Models
{
    public class PlayerData
    {
        public string Name { get; private set; }
        public int NumberOfGames { get; private set; }
        public int TotalAttempts { get; private set; }

        public PlayerData(string name, int attempts)
        {
            if(name == null) throw new ArgumentNullException(nameof(name));
            this.Name = name;
            NumberOfGames = 1;
            TotalAttempts = attempts;
        }

        public void UpdateGame(int attempts)
        {
            TotalAttempts += attempts;
            NumberOfGames++;
        }

        public double CalculateAverageAttempts()
        {
            return (double)TotalAttempts / NumberOfGames;
        }

        public override bool Equals(Object? p)
        {
            if (p == null || !(p is PlayerData))
            {
                return false;
            }
            return Name.Equals(((PlayerData)p).Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
