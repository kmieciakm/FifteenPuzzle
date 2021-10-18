using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle.Heuristics
{
    public interface IHeuristic
    {
        public float Calculate(IPuzzle puzzle, IPuzzle puzzleSolved);
    }
}
