using Puzzle.Heuristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle.Solver
{
    public interface IPuzzleSolver
    {
        /// <summary>
        /// Tries to solve the puzzle.
        /// </summary>
        /// <param name="puzzle">The puzzle to solve.</param>
        /// <param name="steps">Presents the solution found - sequence of actions solving the puzzle. It is assumed, that letter 'L' denotes a move of a piece having freedom to the left, R to the right, U up, and D down.</param>
        /// <returns>True if successfully solved puzzle, otherwise false.</returns>
        bool Solve(ref IPuzzle puzzle, out string steps);
    }

    public interface IInformedPuzzleSolver
    {
        /// <summary>
        /// Tries to solve the puzzle using heuristics.
        /// </summary>
        /// <param name="puzzle">The puzzle to solve.</param>
        /// <param name="steps">Presents the solution found - sequence of actions solving the puzzle. It is assumed, that letter 'L' denotes a move of a piece having freedom to the left, R to the right, U up, and D down.</param>
        /// <returns>True if successfully solved puzzle, otherwise false.</returns>
        bool Solve(ref IPuzzle puzzle, IHeuristic heuristic, out string steps);
    }
}
