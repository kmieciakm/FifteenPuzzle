using System;
using System.Collections.Generic;
using DeepCopy;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle.Solver
{
    interface IPuzzleSolver
    {
        /// <summary>
        /// Tries to solve the puzzle.
        /// </summary>
        /// <param name="puzzle">The puzzle to solve.</param>
        /// <returns>True if successfully solved puzzle, otherwise false.</returns>
        bool Solve(ref IPuzzle puzzle);
    }
}
