using DeepCopy;
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
        public bool Solve(ref IPuzzle puzzle)
        {
            HashSet<IPuzzle> visited = new();
            Queue<IPuzzle> toVisit = new();

            toVisit.Enqueue(puzzle);
            while (toVisit.Count > 0)
            {
                puzzle = toVisit.Dequeue();
                visited.Add(puzzle);

                var possibleMoves = puzzle.GetPossibleMoves();
                IPuzzle nextPuzzle;
                foreach (var move in possibleMoves)
                {
                    nextPuzzle = DeepCopier.Copy(puzzle);
                    nextPuzzle.TryMakeMove(move);
                    if (!visited.Contains(nextPuzzle, Comparer))
                    {
                        toVisit.Enqueue(nextPuzzle);
                    }
                }

                if (puzzle.IsSolved())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
