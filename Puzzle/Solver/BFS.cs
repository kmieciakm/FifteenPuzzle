using Puzzle.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle.Solver
{
    public class BFS : IPuzzleSolver
    {
        private PuzzleComparer Comparer { get; set; } = new PuzzleComparer();

        /// <summary>
        /// The algorithm explores all possible moves of the initial puzzle before continuing with the resulting puzzle.
        /// </summary>
        /// <param name="puzzle">Puzzle to solve.</param>
        /// <returns>True if found solved puzzle otherwise, false.</returns>
        public bool Solve(ref IPuzzle puzzle, out string steps)
        {
            HashSet<IPuzzle> visited = new();
            Queue<IPuzzle> toVisit = new();
            StringBuilder stepsBuilder = new();

            toVisit.Enqueue(puzzle);
            while (toVisit.Count > 0)
            {
                puzzle = toVisit.Dequeue();
                visited.Add(puzzle);

                if (puzzle.IsSolved())
                {
                    steps = stepsBuilder.ToString();
                    return true;
                }

                var possibleMoves = puzzle.GetPossibleMoves();
                IPuzzle nextPuzzle;
                foreach (var move in possibleMoves)
                {
                    nextPuzzle = puzzle.GetCopy();
                    nextPuzzle.TryMakeMove(move);
                    if (!visited.Contains(nextPuzzle, Comparer))
                    {
                        stepsBuilder.Append(move.ToString());
                        toVisit.Enqueue(nextPuzzle);
                    }
                }
            }

            steps = string.Empty;
            return false;
        }
    }
}
