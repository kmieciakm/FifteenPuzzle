using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle.Helpers
{
    public class PuzzleComparer : IEqualityComparer<IPuzzle>
    {
        public bool Equals(IPuzzle puzzle1, IPuzzle puzzle2)
        {
            return puzzle1.Equals(puzzle2);
        }

        public int GetHashCode([DisallowNull] IPuzzle puzzle)
        {
            return puzzle.GetHashCode();
        }
    }
}
