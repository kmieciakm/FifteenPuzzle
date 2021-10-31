using Puzzle.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle.Solver
{
    public class DFS : IPuzzleSolver
    {
        private Stack<IPuzzle> Visited { get; set; } = new Stack<IPuzzle>();
        private PuzzleComparer Comparer { get; set; } = new PuzzleComparer();
        private StringBuilder StepsBuilder { get; set; } = new StringBuilder();

        /// <summary>
        /// The algorithm makes the first move and explores the resulting puzzle as far as possible before backtracking.
        /// </summary>
        /// <param name="puzzle">Puzzle to solve.</param>
        /// <param name="puzzle">Solution steps.</param>
        /// <returns>True if found solved puzzle otherwise, false.</returns>
        public bool Solve(ref IPuzzle puzzle, out string steps)
        {
            Visited.Push(puzzle);

            if (puzzle.IsSolved())
            {
                steps = StepsBuilder.ToString();
                return true;
            }

            var possibleMoves = puzzle.GetPossibleMoves();
            IPuzzle nextPuzzle;
            foreach (var move in possibleMoves)
            {
                nextPuzzle = puzzle.GetCopy();
                nextPuzzle.TryMakeMove(move);
                if (!Visited.Contains(nextPuzzle, Comparer))
                {
                    StepsBuilder.Append(move.ToString());
                    if (Solve(ref nextPuzzle, out steps))
                    {
                        puzzle = nextPuzzle;
                        return true;
                    }
                }
            }

            steps = string.Empty;
            return false;
        }
    }
}
