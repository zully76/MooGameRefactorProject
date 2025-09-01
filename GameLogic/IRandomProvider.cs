using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooGameRefactorProject.GameLogic
{
    public interface IRandomProvider
    {
        int Next(int maxExclusive);
    }
}
